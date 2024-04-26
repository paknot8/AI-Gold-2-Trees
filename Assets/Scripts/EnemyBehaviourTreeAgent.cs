using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourTreeAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    private NavMeshAgent enemyAgent;
    public Transform player;
    public GameObject bulletPrefab;
    public List<Transform> waypoints;

    [Header("Ranges")]
    public float maxDetectionRange = 30f;
    public float bulletFireDistance = 12f;
    public float enemyToPlayerDistance = 7f;

    private void CreateBehaviourTree()
    {
        // This is an AND gate.
        List<IBaseNode> children = new()
        {
            new DetectionNode(enemyAgent,player,maxDetectionRange), // Just to initialize the max detectionDistance (always true)
            new DebugNode("Detecting Player"),

            new PatrolNode(enemyAgent, waypoints), // If this is false then go to next node
            new DebugNode("Moving to the Waypoint"),
            
            new ShootNode(enemyAgent,player,bulletPrefab,bulletFireDistance),
            new DebugNode("Shoot a Bullet"),

            new MoveAwayNode(enemyAgent,player,enemyToPlayerDistance),
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
