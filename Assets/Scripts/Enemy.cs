using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IBaseNode behaviourTree = null;
    private NavMeshAgent agent;
    private Color originalColor;
    
    public GameObject bulletPrefab;
    public Item item;
    public List<Transform> waypoints;

    [Header("Distances")]
    public float pickupDetectionDistance = 20f;
    public float attackDistance = 15f;
    public float moveAwayDistance = 8f;
    public float tooCloseDistance = 4f;
    
    private void CreateBehaviourTree()
    {
        // When the player is too close in range, then speedup, the agent will run faster,
        // but if the agent is stuck, then the agent will tell the player to move away.
        List<IBaseNode> IsPlayerTooClose = new()
        {
            new SprintNode(agent,tooCloseDistance),
            new StandStilNode(agent,tooCloseDistance),
        };

        // When player is too near, check if the player is within the distance then retreat for a little bit,
        // when the player is too close do the check.
        List<IBaseNode> IsPlayerNearby = new()
        {
            new RetreatNode(agent,moveAwayDistance),
            new SequenceNode(IsPlayerTooClose),
        };

        //  when the player is within the shooting distance, Keep shooting at the player,
        // but when the player is too near of the agent then do the check.
        List<IBaseNode> IsPlayerWithinShootingRange = new()
        {
            new ShootNode(agent,bulletPrefab,attackDistance,moveAwayDistance),
            new SequenceNode(IsPlayerNearby),
        };

        // Is the player in sight, then chase player
        // but when the player is within the shooting range then do the check
        List<IBaseNode> IsPlayerInLineOfSight = new()
        {
            new ChaseNode(agent,attackDistance,moveAwayDistance),
            new SequenceNode(IsPlayerWithinShootingRange),
        };

        // Is the item in sight then pick it up.
        List<IBaseNode> IsItemInLineOfSight = new()
        {
            new PickupNode(agent,pickupDetectionDistance,item),
        };

        // Is the player not in sight, then either patrol, but when the item is in sight, priorize the Item to pickup.
        List<IBaseNode> IsPlayerNotInLineOfSight = new()
        {
            new PatrolNode(agent,waypoints,attackDistance,pickupDetectionDistance,item),
            new SequenceNode(IsItemInLineOfSight),
        };

        // Makes choice between Player in sight or out of sight.
        List<IBaseNode> Root = new()
        {
            new SequenceNode(IsPlayerInLineOfSight), 
            new SequenceNode(IsPlayerNotInLineOfSight),
        };

        behaviourTree = new SelectorNode(Root); // Gets the final decision of the Agent.
    }

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
        else
        {
            Debug.Log("Agent does not exists.");
        }
    }

    // Restore original color when needed
    public void RestoreOriginalColor() => GetComponent<Renderer>().material.color = originalColor;
}
