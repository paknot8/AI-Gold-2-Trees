using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This node, iterates over children and executes the children from start to finish
// if one of the children fails the whole node fails
// if succeeds it will exit to that point
public class SequencerNode : CompositeNode
{
    int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        var child = children[current];

        switch (child.Update()){
            case State.Running: // if child not done continue excuting
                return State.Running;
            case State.Failure: // if child fails then return failure stop executing
                break;
            case State.Success: // if child succeeds, increment and move to next child to execute
                current++;
                break;
        }
        return current == children.Count ? State.Success : State.Running;
    }
}
