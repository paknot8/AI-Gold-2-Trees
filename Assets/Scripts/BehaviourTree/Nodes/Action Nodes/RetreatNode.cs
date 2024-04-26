using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float moveAwayDistance;
    private float timeStuck = 0.0f;
    private Vector3 lastStuckPosition;

    public RetreatNode(NavMeshAgent agent, Transform player, float moveAwayDistance)
    {
        this.agent = agent;
        this.player = player;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        // Calculate range from stopping distance
        float agentToPlayerDistance = Vector3.Distance(agent.transform.position, player.position);

        // Check if player is within range
        if (agentToPlayerDistance <= moveAwayDistance)
        {
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();

            if (IsTargetValidOnNavMesh(targetPosition))
            {
                agent.SetDestination(targetPosition);
                ResetWhenStuck();
            }
        }
        return true;
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        agent.GetComponent<Renderer>().material.color = Color.yellow;

        Vector3 retreatDirection = (agent.transform.position - player.position).normalized; // Move away from the player
        retreatDirection.y = 0f;

        Vector3 targetPosition = agent.transform.position + retreatDirection * 3f; // Move 3 units away from the player

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // If not valid, return the agent's position (fallback)
        return agent.transform.position;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition)
    {
        return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }

    private void ResetWhenStuck()
    {
        if (agent.transform.position == lastStuckPosition)
        {
            timeStuck += Time.deltaTime;
            if (timeStuck > 1.0f) // Adjust threshold based on your needs
            {
                // Agent is stuck for more than 1 second, reset position or choose new direction
                agent.Warp(agent.transform.position + Random.insideUnitSphere * 2.0f); // Randomly move the agent a bit
                timeStuck = 0.0f;
                lastStuckPosition = Vector3.zero;
            }
        }
        else
        {
            timeStuck = 0.0f;
            lastStuckPosition = agent.transform.position;
        }
    }
}
