using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourTreeAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;

    private NavMeshAgent agent;
    public Transform player;
    public GameObject bulletPrefab;
    public List<Transform> waypoints;
    
    private Color originalColor; // Variable to store the original color

    [Header("Ranges")]
    public float maxDetectionRange = 20f;
    public float attackDistance = 15f;
    public float moveAwayDistance = 10f;
    
    // Sequence node, executes child node, one after another. 
    // if any child node returns false, the sequence node returns false itself
    // and no further chuld nodes are evaluated (continued).
    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            new DetectionNode(agent,player,maxDetectionRange), // Just to initialize the max detectionDistance (always true)
            new PatrolNode(agent,waypoints,player),
            new RetreatNode(agent,player,moveAwayDistance),
            new ChaseNode(agent,player,attackDistance,moveAwayDistance),
            new ShootNode(agent,player,bulletPrefab,attackDistance,moveAwayDistance),
        };

        int currentNodeIndex = 0;
        foreach (IBaseNode node in children)
        {
            if (!node.Update())
            {
            Debug.Log("Node " + currentNodeIndex + " (" + node.GetType().Name + ") returned false.");
            break;
            }
            currentNodeIndex++;
        }

        behaviourTree = new SequenceNode(children);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (agent != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(agent.transform.position, maxDetectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.transform.position, attackDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(agent.transform.position, moveAwayDistance);
        }
    }

    // Restore original color when needed
    public void RestoreOriginalColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
