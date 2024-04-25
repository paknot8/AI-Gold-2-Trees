using UnityEngine;
using UnityEngine.AI;

public class MoveAwayNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform player;
    private float moveAwayDistance; // Default distance from stopping distance of agent
    private float stuckTimer = 3; // Time to consider agent stuck
    private Vector3 previousPosition; // Track previous position

    public MoveAwayNode(NavMeshAgent enemyAgent, Transform playerTransform, float moveAwayDistance)
    {
        this.enemyAgent = enemyAgent;
        player = playerTransform;
        this.moveAwayDistance = moveAwayDistance;
        previousPosition = enemyAgent.transform.position;
    }

    public virtual bool Update()
    {
        // Calculate range from stopping distance
        float rangeFromStoppingDistance = enemyAgent.stoppingDistance + moveAwayDistance;

        // Check if player is within range
        if (Vector3.Distance(enemyAgent.transform.position, player.position) <= rangeFromStoppingDistance)
        {
            // Calculate direction and target position away from player
            Vector3 moveDirection = enemyAgent.transform.position - player.position;
            moveDirection.y = 0f;
            moveDirection.Normalize();
            Vector3 targetPosition = enemyAgent.transform.position + moveDirection * moveAwayDistance;

            // Check if target is valid on NavMesh
            if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                enemyAgent.SetDestination(targetPosition);
                previousPosition = enemyAgent.transform.position; // Reset previous position
                stuckTimer = 0; // Reset stuck timer
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Check for stuck state (choose your method - time or distance)
            if (IsAgentStuck())
            {
                return false;
            }
            return false;
        }
    }

    private bool IsAgentStuck()
    {
        float distance = Vector3.Distance(enemyAgent.transform.position, previousPosition);
        stuckTimer -= Time.deltaTime;
        return (distance < 0.1f && stuckTimer <= 0); // Adjust thresholds as needed
    }
}
