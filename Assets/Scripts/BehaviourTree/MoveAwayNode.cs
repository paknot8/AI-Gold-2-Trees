using UnityEngine;
using UnityEngine.AI;

public class MoveAwayNode : IBaseNode
{
    private readonly NavMeshAgent enemyAgent;
    private readonly Transform player;
    private readonly float moveAwayDistance;
    private float timeToConsiderStuck = 3f; // Time to consider agent stuck
    private Vector3 previousPosition;

    public MoveAwayNode(NavMeshAgent enemyAgent, Transform playerTransform, float moveAwayDistance)
    {
        this.enemyAgent = enemyAgent;
        player = playerTransform;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        // Calculate range from stopping distance
        float rangeFromStoppingDistance = enemyAgent.stoppingDistance + moveAwayDistance;

        // Check if player is within range
        if (IsPlayerWithinRange(rangeFromStoppingDistance))
        {
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();
            if (IsTargetValidOnNavMesh(targetPosition))
            {
                enemyAgent.SetDestination(targetPosition);
                ResetStuckTimer();
                return true;
            }
        }
        else
        {
            if (IsAgentStuck())
            {
                return false;
            }
        }

        return false;
    }

    private bool IsPlayerWithinRange(float range)
    {
        return Vector3.Distance(enemyAgent.transform.position, player.position) <= range;
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        Vector3 moveDirection = enemyAgent.transform.position - player.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();
        return enemyAgent.transform.position + moveDirection * moveAwayDistance;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition)
    {
        return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }

    private void ResetStuckTimer()
    {
        previousPosition = enemyAgent.transform.position;
        timeToConsiderStuck = 3f;
    }

    private bool IsAgentStuck()
    {
        float distance = Vector3.Distance(enemyAgent.transform.position, previousPosition);
        timeToConsiderStuck -= Time.deltaTime;
        return (distance < 0.1f && timeToConsiderStuck <= 0);
    }
}
