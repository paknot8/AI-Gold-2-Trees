using UnityEngine;
using UnityEngine.AI;

public class SprintNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;
    private readonly float tooCloseDistance;
    private readonly float originalSpeed;

    public SprintNode(NavMeshAgent agent, float tooCloseDistance)
    {
        this.agent = agent;
        this.tooCloseDistance = tooCloseDistance;
        this.originalSpeed = agent.speed; // Store the original speed
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        float agentToPlayerDistance = Vector3.Distance(agent.transform.position, playerPosition);

        if (agentToPlayerDistance <= tooCloseDistance)
        {
            Blackboard.instance.SetIndicatorText("I am faster than you!");
            agent.speed = 20f; // Increase speed when too close
            if(agentToPlayerDistance <= tooCloseDistance && agent.velocity.magnitude < 0.01f)
            {
                Blackboard.instance.SetIndicatorText("Damn It! You Got me Cornered!");
            }
        }
        else if (agentToPlayerDistance >= tooCloseDistance)
        {
            agent.speed = originalSpeed; // Revert to original speed when not too close
        }
        else
        {
            return false;
        }
        return true;
    }
}
