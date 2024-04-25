using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    private NavMeshAgent enemyAgent;
    public List<Transform> waypoints;

    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            new WalkNode("Door"),
            new PatrolNode(enemyAgent,waypoints), // als dit false return dan gaat het niet door "FAIL all below"
            new WalkNode("Kitchen"),
        };
        behaviourTree = new SequenceNode(children);
    }

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        CreateBehaviourTree();
    }

    void Update()
    {
        behaviourTree?.Update();
    }
}
