using UnityEngine;
using UnityEngine.AI;

public class RetreatNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;
    private Vector3 lastPosition;
    private readonly float moveAwayDistance;
    private float timeStuck = 0.0f;
    private float agentToPlayerDistance;

    public RetreatNode(NavMeshAgent agent, float moveAwayDistance)
    {
        this.agent = agent;
        this.moveAwayDistance = moveAwayDistance;
        this.lastPosition = agent.transform.position;
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        agentToPlayerDistance = Vector3.Distance(agent.transform.position, playerPosition);

        if (agentToPlayerDistance <= moveAwayDistance)
        {
            
            Vector3 targetPosition = CalculateTargetPositionAwayFromPlayer();

            if (IsTargetValidOnNavMesh(targetPosition))
            {
                Blackboard.instance.SetIndicatorText("Aaah! Don't come closer!");
                agent.SetDestination(targetPosition);
            }
            // Check for being stuck
            CheckStuck();
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

    private void CheckStuck()
    {
        if (agent.transform.position == lastPosition)
        {
            timeStuck += Time.deltaTime;
            if (timeStuck > 1.0f) // Adjust threshold based on your needs
            {
                // Agent is stuck for more than 1 second, reset position or choose new direction
                agent.Warp(agent.transform.position + Random.insideUnitSphere * 2.0f); // Randomly move the agent a bit
                timeStuck = 0.0f;
                lastPosition = agent.transform.position;
            }
        }
        else
        {
            timeStuck = 0.0f;
            lastPosition = agent.transform.position;
        }
    }
}
