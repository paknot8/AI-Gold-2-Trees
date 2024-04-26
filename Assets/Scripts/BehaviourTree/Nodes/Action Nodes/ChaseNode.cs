using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float attackDistance;
    private readonly float moveAwayDistance;
    private Vector3 lastPlayerPosition; // Store the player's position before resetting the path

    public ChaseNode(NavMeshAgent agent, Transform player, float attackDistance, float moveAwayDistance)
    {
        this.agent = agent;
        this.player = player;
        this.attackDistance = attackDistance;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        if (Vector3.Distance(agent.transform.position, player.position) < attackDistance
            && Vector3.Distance(agent.transform.position, player.position) > moveAwayDistance)
        {
            lastPlayerPosition = player.position; // Store the player's position
            agent.SetDestination(lastPlayerPosition);
            agent.GetComponent<Renderer>().material.color = Color.red;
            return true;
        }
        else
        {
            return false;
        } 
    }
}
