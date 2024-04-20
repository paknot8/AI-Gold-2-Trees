using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add a delay in the tree
public class WaitNode : ActionNode
{
    public float duration = 3f;
    public float startTime;

    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
