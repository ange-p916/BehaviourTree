using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    public float speed;
    public Transform target;

    public Selector m_rootNode;
    public Selector Combat;

    public ActionNode Melee;
    public ActionNode Parry;
    public ActionNode Evade;

    public Sequence ChaseTarget;
    public ActionNode BT_ChaseTarget;
    public ActionNode BT_MoveTo;
    public ActionNode BT_CheckDistance;
    public ActionNode BT_ColorCheck;

    public string valueLabel;
    List<Node> rootChildren;

    public Text DistanceDebugText;
    public Text DistanceNodeCheck;
    public Text SpeedNodeCheck;
    public Text ColorNodeCheck;
    public Text MoveToNodeCheck;
    public bool StartMoving = false;
    public List<Node> ChaseNodes = new List<Node>();
    void Start()
    {
        //Not current
        /*Build tree starting from the bottom node, left to right
                root
                /  \
           Combat        Idle
          /   |     \        \
      Melee  Parry  Evade     Patrol
        */

        /*Build tree starting from the bottom node, left to right
                root
                /  
             Chase        
          /     |    \            \        
        Dist  Speed  ColorCheck  MoveTo 
        */


        BT_CheckDistance = new ActionNode(CheckDistanceToPlayer);
        BT_ChaseTarget = new ActionNode(SetSpeed);
        BT_ColorCheck = new ActionNode(IsColorBlue);
        BT_MoveTo = new ActionNode(MoveTo);

        ChaseNodes.Add(BT_CheckDistance);
        ChaseNodes.Add(BT_ChaseTarget);
        ChaseNodes.Add(BT_ColorCheck);
        ChaseNodes.Add(BT_MoveTo);

        ChaseTarget = new Sequence(ChaseNodes);
        //ChaseTarget = new Sequence(ChaseNodes() { 
        //    BT_CheckDistance,
        //    BT_ChaseTarget,
        //    BT_ColorCheck,
        //    BT_MoveTo
        //});

        rootChildren = new List<Node>();
        rootChildren.Add(ChaseTarget);

        m_rootNode = new Selector(rootChildren);

        //StartCoroutine(MoveToPos());
        
    }

    void Update()
    {
        DistanceNodeCheck.text = CheckDistanceToPlayer().ToString();
        SpeedNodeCheck.text = SetSpeed().ToString();
        ColorNodeCheck.text = IsColorBlue().ToString();
        MoveToNodeCheck.text = MoveTo().ToString();
        DistanceDebugText.text = "Distance to target: " + Vector2.Distance(target.transform.position, this.transform.position);

        foreach (var item in ChaseNodes)
        {
            print("cur state: " + item.nodeState);
        }
        if (ChaseTarget.nodeState == NodeStates.SUCCESS)
            StartMoving = true;

        if(StartMoving)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        }
        m_rootNode.Evaluate();
    }

    private NodeStates MoveTo()
    {
        if(target != null)
        {
            return NodeStates.SUCCESS;
        }        
        else
        {
            return NodeStates.FAILURE;
        }
            
    }

    private NodeStates IsColorBlue()
    {
        if (target.GetComponent<SpriteRenderer>().color.Equals(Color.blue))
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates SetSpeed()
    {
        if (CheckDistanceToPlayer() == NodeStates.SUCCESS)
            speed = 5f;
        else
            speed = 0f;

        if (speed >= 5f)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }


    private NodeStates AtPosition()
    {
        if (Vector2.Distance(target.transform.position, this.transform.position) < 2f)
        {
            StartMoving = false;
            return NodeStates.SUCCESS;
        }
        else
            return NodeStates.FAILURE;
    }



    private NodeStates CheckDistanceToPlayer()
    {
        if (Vector2.Distance(target.transform.position, this.transform.position) < 10f)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }
}
