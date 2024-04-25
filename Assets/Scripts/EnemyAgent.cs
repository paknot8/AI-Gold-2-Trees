using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            new WalkNode("Door"),
            new PatrolNode(),
            new WalkNode("Kitchen"),
        };
        behaviourTree = new SequenceNode(children);
    }

    void Start()
    {
        CreateBehaviourTree();
    }

    void Update()
    {
        behaviourTree?.Update();
    }
}
