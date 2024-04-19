using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthnode : Node
{
    private EnemyAI ai;
    private float threshold;

    public Healthnode(EnemyAI ai, float threshold){
        this.ai = ai;
        this.threshold = threshold;
    }

    public override NodeState Evaluate()
    {
        return ai.GetCurrentHealth() <= threshold ? NodeState.FAILURE : NodeState.SUCCES;
    }
}
