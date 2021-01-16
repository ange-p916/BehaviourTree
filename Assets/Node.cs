using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeStates
{
    FAILURE,
    SUCCESS,
    RUNNING
}

[System.Serializable]
public abstract class Node
{
    // Delegate that returns state of the node
    public delegate NodeStates NodeReturn();

    //The current state of the ndode
    protected NodeStates m_nodeState;

    public NodeStates nodeState
    {
        get { return m_nodeState; }
    }

    //constructor for node
    public Node() { }

    //Implementing classes use this method to evalute the desired set of conditions
    public abstract NodeStates Evaluate();

}
