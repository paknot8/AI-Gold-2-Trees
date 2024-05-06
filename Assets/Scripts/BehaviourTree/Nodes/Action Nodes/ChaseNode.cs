using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;
    private readonly float attackDistance;
    private readonly float moveAwayDistance;
    private Vector3 lastPlayerPosition; // Store the player's position before resetting the path

    public ChaseNode(NavMeshAgent agent, float attackDistance, float moveAwayDistance)
    {
        this.agent = agent;
        this.attackDistance = attackDistance;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        if (Vector3.Distance(agent.transform.position, playerPosition) < attackDistance
            && Vector3.Distance(agent.transform.position, playerPosition) > moveAwayDistance)
        {
            Blackboard.instance.SetIndicatorText("I'm Gonna Get Ya!");
            lastPlayerPosition = playerPosition; // Store the player's position
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
