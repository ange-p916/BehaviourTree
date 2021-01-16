using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Transform target;

    public Selector m_rootNode;
    public ActionNode Node2;
    //public Limit
    public ActionNode Node3;

    public string valueLabel;
    List<Node> rootChildren;

    // Start is called before the first frame update
    void Start()
    {
        Node3 = new ActionNode(Move);
        Node2 = new ActionNode(AtPosition);
        rootChildren = new List<Node>();

        m_rootNode = new Selector(rootChildren);
        
        StartCoroutine(MoveToPos());
        m_rootNode.Evaluate();
    }

    // Update is called once per frame
    void Update()
    {
        print(AtPosition());
    }

    private IEnumerator MoveToPos()
    {
        while(AtPosition() == NodeStates.FAILURE)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, 5f * Time.deltaTime);
            yield return null;
        }
    }

    private NodeStates AtPosition()
    {
        bool inRangeOfTarget = (Vector2.Distance(target.transform.position, this.transform.position) < 2f);
        if (inRangeOfTarget)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates Move()
    {
        bool hasTarget = (Vector2.Distance(target.transform.position, this.transform.position) < 10f);
        if (hasTarget)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }
}
