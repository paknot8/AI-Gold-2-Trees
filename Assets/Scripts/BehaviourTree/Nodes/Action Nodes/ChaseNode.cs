using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float attackDistance;

    public ChaseNode(NavMeshAgent agent, Transform player, float attackDistance)
    {
        this.agent = agent;
        this.player = player;
        this.attackDistance = attackDistance;
    }

    public virtual bool Update()
    {
        if (IsPlayerWithinAttackDistance())
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

    private bool IsPlayerWithinAttackDistance()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
        return distanceToPlayer <= attackDistance;
    }
}
