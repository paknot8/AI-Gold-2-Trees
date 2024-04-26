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
    private Color originalColor; // Variable to store the original color

    [Header("Ranges")]
    public float maxDetectionRange = 20f;
    public float bulletFireDistance = 15f;
    public float moveAwayDistance = 5f;

    public static bool isPatrolling = false;

    private void CreateBehaviourTree()
    {
        // This is an AND gate
        // If a child is false it will not execute
        List<IBaseNode> children = new()
        {
            new DetectionNode(enemyAgent,player,maxDetectionRange), // Just to initialize the max detectionDistance (always true)
            
            new PatrolNode(enemyAgent,waypoints),

            new ShootNode(enemyAgent,player,bulletPrefab,bulletFireDistance),
            new DebugNode("Shoot a Bullet"),

            new RetreatNode(enemyAgent,player,moveAwayDistance),
            new DebugNode("MoveAway"),
        };
        behaviourTree = new SequenceNode(children);
    }

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        originalColor = GetComponent<Renderer>().material.color; // Save the original color
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

    // Restore original color when needed
    public void RestoreOriginalColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
