using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    private NavMeshAgent enemyAgent;
    public Transform player;
    public GameObject bulletPrefab;
    public List<Transform> waypoints;

    // This is an AND gate.
    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            new DetectionNode(enemyAgent,player,30f), // Just to initialize the max detectionDistance
            new DebugNode("Detecting Player"),
            new PatrolNode(enemyAgent, waypoints), // If this is false then go to next node
            new DebugNode("Moving to the Waypoint"),
            new ShootNode(enemyAgent,player,bulletPrefab,10f),
            new DebugNode("Shoot a Bullet"),
            new MoveAwayNode(enemyAgent,player,5f),
            new DebugNode("MoveAway"),
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
