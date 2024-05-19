using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;

    // --- Distances ---
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
        return false;
    }

    private Vector3 CalculateTargetPositionAwayFromPlayer()
    {
        SetAgentColor(Color.yellow);

        Vector3 retreatDirection = GetRetreatDirection();
        Vector3 targetPosition = agent.transform.position + retreatDirection * 3f;

        if (IsTargetValidOnNavMesh(targetPosition))
        {
            return targetPosition;
        }

        // If the target position is not valid on the NavMesh, return the agent's current position
        return agent.transform.position;
    }

    private bool IsTargetValidOnNavMesh(Vector3 targetPosition){
         return NavMesh.SamplePosition(targetPosition, out _, 1.0f, NavMesh.AllAreas);
    }

    private void SetAgentColor(Color color) => agent.GetComponent<Renderer>().material.color = color;

    private Vector3 GetRetreatDirection()
    {
        Vector3 retreatDirection = (agent.transform.position - Blackboard.instance.GetPlayerPosition()).normalized;
        retreatDirection.y = 0f; // Ensure movement is in the horizontal plane
        return retreatDirection;
    }
}
