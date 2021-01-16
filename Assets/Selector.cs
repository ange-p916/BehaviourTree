using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    //child nodes for this selector
    protected List<Node> m_nodes = new List<Node>();

    //pass nodes to constructor
    public Selector (List<Node> nodes)
    {
        m_nodes = nodes;
    }

    //if any child succeed; report success upwards, else report failure
    public override NodeStates Evaluate()
    {
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = NodeStates.FAILURE;
        return m_nodeState;
    }




}
