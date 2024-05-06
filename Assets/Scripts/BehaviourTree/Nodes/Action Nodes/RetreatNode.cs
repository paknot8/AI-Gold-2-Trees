using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;
    private readonly float moveAwayDistance;

    public RetreatNode(NavMeshAgent agent, float moveAwayDistance)
    {
        this.agent = agent;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        
        if (Vector3.Distance(agent.transform.position, playerPosition) <= moveAwayDistance)
        {
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();
            if (IsTargetValidOnNavMesh(targetPosition))
            {
                Blackboard.instance.SetIndicatorText("Retreating...");
                agent.SetDestination(targetPosition);
            }
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        agent.GetComponent<Renderer>().material.color = Color.yellow;

        Vector3 retreatDirection = (agent.transform.position - Blackboard.instance.GetPlayerPosition()).normalized;
        retreatDirection.y = 0f;

        Vector3 targetPosition = agent.transform.position + retreatDirection * 3f;

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return agent.transform.position;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition)
    {
        return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }
}
