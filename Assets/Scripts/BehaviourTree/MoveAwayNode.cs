using UnityEngine;
using UnityEngine.AI;

public class MoveAwayNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform player;
    private float moveAwayDistance; // Default distance from stopping distance of agent

    public MoveAwayNode(NavMeshAgent enemyAgent, Transform playerTransform, float moveAwayDistance)
    {
        this.enemyAgent = enemyAgent;
        player = playerTransform;
        this.moveAwayDistance = moveAwayDistance;
    }

    public bool Update()
    {
        // Calculate the range from the stopping distance of the agent
        float rangeFromStoppingDistance = enemyAgent.stoppingDistance + moveAwayDistance;

        // Check if player is within range
        if (Vector3.Distance(enemyAgent.transform.position, player.position) <= rangeFromStoppingDistance)
        {
            // Calculate direction away from the player
            Vector3 moveDirection = enemyAgent.transform.position - player.position;
            moveDirection.y = 0f; // Optional: Keep the movement in the horizontal plane only
            moveDirection.Normalize();

            // Calculate the position to move away from the player
            Vector3 targetPosition = enemyAgent.transform.position + moveDirection * moveAwayDistance;

            // Move towards the target position
            enemyAgent.SetDestination(targetPosition);
            return true;
        }
        else 
        {
            return false;
        }
    }

    // Draw gizmos to visualize range from the player and the position where the enemy will move away to
    private void OnDrawGizmos()
    {
        // Draw range from the player
        float rangeFromStoppingDistance = enemyAgent.stoppingDistance + moveAwayDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyAgent.transform.position, rangeFromStoppingDistance);

        // Draw position where the enemy will move away to
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(enemyAgent.transform.position + Vector3.up * 0.1f, 1f);
    }
}
