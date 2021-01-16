using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    //method signature for the action
    public delegate NodeStates ActionNodeDelegate();

    //the delegate that is called to evaluate this node
    private ActionNodeDelegate m_action;

    //no logic in the node
    //pass logic in form of a delegate
    //action should return a nodestates enum
    public ActionNode(ActionNodeDelegate action)
    {
        m_action = action;
    }
    
    //evaluate the node by passing in the delegate and report the state
    //as appropriate
    public override NodeStates Evaluate()
    {
        switch (m_action())
        {
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
        }
    }
}
