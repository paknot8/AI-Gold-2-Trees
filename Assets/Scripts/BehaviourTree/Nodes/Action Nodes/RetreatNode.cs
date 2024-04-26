using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float moveAwayDistance;
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
        float agentToPlayerDistance = Vector3.Distance(agent.transform.position, player.position);

        // Check if player is within range
        if (agentToPlayerDistance <= moveAwayDistance)
        {
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();
            if (IsTargetValidOnNavMesh(targetPosition))
            {
                agent.SetDestination(targetPosition);
                ResetWhenStuck();
                return true;
            }
        }

        return false;
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        agent.GetComponent<Renderer>().material.color = Color.yellow;
        Vector3 moveDirection = agent.transform.position - player.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();
        return agent.transform.position + moveDirection * moveAwayDistance;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition)
    {
        return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }

    private void ResetWhenStuck()
    {
        previousPosition = agent.transform.position;
    }
}
