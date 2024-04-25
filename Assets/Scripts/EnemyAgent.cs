using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    public Transform playerTransform;
    private NavMeshAgent enemyAgent;
    public List<Transform> waypoints;

    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            new PatrolNode(enemyAgent,waypoints), // als dit false return dan gaat het niet door "FAIL all below"
            new WalkNode("Moving to the Waypoint"),
            new WalkNode("1"),
            new MoveAwayNode(enemyAgent, playerTransform, 5f),
            new WalkNode("MoveAway"),
            new WalkNode("2"),
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
