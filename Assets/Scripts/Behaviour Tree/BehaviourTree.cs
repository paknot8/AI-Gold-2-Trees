using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public BaseNode rootNode;
    public BaseNode.State treeState = BaseNode.State.Running;

    public BaseNode.State Update()
    {
        if(rootNode.state == BaseNode.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }
}


