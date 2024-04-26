using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float attackDistance;
    private readonly float moveAwayDistance;

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
            agent.SetDestination(player.position);
            agent.GetComponent<Renderer>().material.color = Color.red;
            return true;
        }
        else
        {
            return false;
        } 
    }
}
