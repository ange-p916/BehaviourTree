using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    //child node, only have 1 per inverter
    private Node m_node;

    public Node node
    {
        get { return m_node; }
    }

    //constructoren skal sette denne child node som decoratoren bruger

    public Inverter(Node node)
    {
        m_node = node;
    }

    //report success if the child fails, and failure if the child succeeds
    public override NodeStates Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
        }
        m_nodeState = NodeStates.SUCCESS;
        return m_nodeState;
    }
}
