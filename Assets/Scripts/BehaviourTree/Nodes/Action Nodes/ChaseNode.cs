using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float attackDistance;
    private readonly float moveAwayDistance;
    private float moveAwayTimer = 0f; // Timer to track how long the agent has been within move away distance
    private float moveAwayDuration = 5f; // Duration for which the agent stays within move away distance

    public ChaseNode(NavMeshAgent agent, Transform player, float attackDistance, float moveAwayDistance)
    {
        this.agent = agent;
        this.player = player;
        this.attackDistance = attackDistance;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        if (IsPlayerWithinMoveAwayDistance())
        {
            // Player is within move away distance, increment the timer
            moveAwayTimer += Time.deltaTime;

            if (moveAwayTimer >= moveAwayDuration)
            {
                // Timer expired, reset timer and do not chase
                moveAwayTimer = 0f;
                return false; // Return false to indicate that the chase behavior is not active
            }
            else
            {
                // Timer still active, do not chase
                return false; // Return false to indicate that the chase behavior is not active
            }
        }
        else if (IsPlayerWithinAttackDistance())
        {
            // Player is within attack distance, chase the player
            agent.SetDestination(player.position);
            agent.GetComponent<Renderer>().material.color = Color.red;
            return true; // Return true to indicate that the chase behavior is active
        }
        else
        {
            // Player is not within attack distance, do not chase
            return false; // Return false to indicate that the chase behavior is not active
        }
    }

    private bool IsPlayerWithinMoveAwayDistance()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
        return distanceToPlayer <= moveAwayDistance;
    }

    private bool IsPlayerWithinAttackDistance()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
        return distanceToPlayer <= attackDistance;
    }
}
