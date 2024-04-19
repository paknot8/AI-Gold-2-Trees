using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    protected Node node;

    public Inverter(Node node)
    {
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCES:
                _nodeState = NodeState.SUCCES;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.FAILURE;
                break;
            default:
                break;
        }
        return _nodeState;
    }
}
