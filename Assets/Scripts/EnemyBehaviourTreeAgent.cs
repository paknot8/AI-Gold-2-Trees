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
    public float maxDetectionRange = 20f;
    public float bulletFireDistance = 15f;
    public float moveAwayDistance = 5f;

    private void CreateBehaviourTree()
    {
        // This is an AND gate.
        List<IBaseNode> children = new()
        {
            new DetectionNode(enemyAgent,player,maxDetectionRange), // Just to initialize the max detectionDistance (always true)

            new PatrolNode(enemyAgent, waypoints), // If this is false then go to next node
            
            new ShootNode(enemyAgent,player,bulletPrefab,bulletFireDistance),
            new DebugNode("Shoot a Bullet"),

            new MoveAwayNode(enemyAgent,player,moveAwayDistance),
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

    // Draw Gizmos to visualize the detection range
    void OnDrawGizmosSelected()
    {
        if (enemyAgent != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(enemyAgent.transform.position, maxDetectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyAgent.transform.position, bulletFireDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(enemyAgent.transform.position, moveAwayDistance);
        }
    }
}
