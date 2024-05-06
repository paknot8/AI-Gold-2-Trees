using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IBaseNode behaviourTree = null;
    private Color originalColor; // Variable to store the original color
    private NavMeshAgent agent;
    public GameObject bulletPrefab;
    public Item item;
    public List<Transform> waypoints;

    [Header("Ranges")]
    private readonly float pickupDetectionDistance = 20f;
    private readonly float attackDistance = 15f;
    private readonly float moveAwayDistance = 8f;
    private readonly float tooCloseDistance = 4f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalColor = GetComponent<Renderer>().material.color; // Save the original color
        CreateBehaviourTree();
    }

    void Update()
    {
        item = FindAnyObjectByType<Item>();
        behaviourTree?.Update();
    }
    
    private void CreateBehaviourTree()
    {
        List<IBaseNode> IsPlayerTooClose = new()
        {
            new SprintNode(agent,tooCloseDistance),
        };

        List<IBaseNode> IsPlayerNearby = new()
        {
            new RetreatNode(agent,moveAwayDistance),
            new SequenceNode(IsPlayerTooClose),
        };

        List<IBaseNode> IsPlayerInLineOfSight = new()
        {
            new ChaseNode(agent,attackDistance,moveAwayDistance),
            new ShootNode(agent,bulletPrefab,attackDistance,moveAwayDistance),
            new SequenceNode(IsPlayerNearby),
        };

        List<IBaseNode> IsItemInLineOfSight = new()
        {
            new PickupNode(agent,pickupDetectionDistance,item),
        };

        List<IBaseNode> IsPlayerNotInLineOfSight = new()
        {
            new PatrolNode(agent,waypoints,attackDistance,pickupDetectionDistance,item),
            new SequenceNode(IsItemInLineOfSight),
        };

        List<IBaseNode> Root = new()
        {
            new SequenceNode(IsPlayerInLineOfSight), 
            new SequenceNode(IsPlayerNotInLineOfSight),
        };

        behaviourTree = new SelectorNode(Root);
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
        
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(agent.transform.position, pickupDetectionDistance);
        }
    }

    // Restore original color when needed
    public void RestoreOriginalColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
