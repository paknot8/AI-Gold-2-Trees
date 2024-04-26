using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourTreeAgent : MonoBehaviour
{
    private IBaseNode behaviourTree = null;
    private Color originalColor; // Variable to store the original color
    private NavMeshAgent agent;
    public Transform player;
    public GameObject bulletPrefab;
    public List<Transform> waypoints;
    
    [Header("Ranges")]
    private readonly float attackDistance = 15f;
    private readonly float moveAwayDistance = 6f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalColor = GetComponent<Renderer>().material.color; // Save the original color
        CreateBehaviourTree();
    }

    void Update() => behaviourTree?.Update();
    
    // Sequence node, executes child node, one after another. 
    // if any child node returns false, the sequence node returns false itself
    // and no further chuld nodes are evaluated (continued).
    private void CreateBehaviourTree()
    {
        List<IBaseNode> children = new()
        {
            //new DetectionNode(agent,player,maxDetectionRange), // Just to initialize the max detectionDistance (always true)
            new PatrolNode(agent,waypoints,player,attackDistance),
            new RetreatNode(agent,player,moveAwayDistance),
            new ChaseNode(agent,player,attackDistance,moveAwayDistance),
            new ShootNode(agent,player,bulletPrefab,attackDistance,moveAwayDistance),
        };

        StatementDebugger(children); // this is only used for testing

        behaviourTree = new SequenceNode(children);
    }

    private static void StatementDebugger(List<IBaseNode> children)
    {
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
    }

    // Draw Gizmos to visualize the detection range
    void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
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
