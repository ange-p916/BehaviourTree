using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    private List<Node> m_nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        m_nodes = nodes;
    }

    //if any child returns a failure, the entire node fails
    //so if all nodes return a success, the node reports success
    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
            }
        }
        m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return m_nodeState;
    }
}
