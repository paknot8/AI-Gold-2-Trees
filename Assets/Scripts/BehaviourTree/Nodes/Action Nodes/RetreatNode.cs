using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float moveAwayDistance;
    private float timeToConsiderStuck = 3f; // Time to consider agent stuck
    private Vector3 previousPosition;

    public RetreatNode(NavMeshAgent agent, Transform player, float moveAwayDistance)
    {
        this.agent = agent;
        this.player = player;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        // Calculate range from stopping distance
        float rangeFromStoppingDistance = agent.stoppingDistance + moveAwayDistance;

        // Check if player is within range
        if (IsPlayerWithinRange(rangeFromStoppingDistance))
        {
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();
            if (IsTargetValidOnNavMesh(targetPosition))
            {
                // Reset color to original if not within range
                agent.GetComponent<Renderer>().material.color = Color.yellow; // Set back to original color
                agent.SetDestination(targetPosition);
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
        return Vector3.Distance(agent.transform.position, player.position) <= range;
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        Vector3 moveDirection = agent.transform.position - player.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();
        return agent.transform.position + moveDirection * moveAwayDistance;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition)
    {
        return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }

    private void ResetStuckTimer()
    {
        previousPosition = agent.transform.position;
        timeToConsiderStuck = 3f;
    }

    private bool IsAgentStuck()
    {
        float distance = Vector3.Distance(agent.transform.position, previousPosition);
        timeToConsiderStuck -= Time.deltaTime;
        return (distance < 0.1f && timeToConsiderStuck <= 0);
    }
}
