using UnityEngine;
using UnityEngine.AI;

public class StandStilNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;

    // --- Distances ---
    private float agentToPlayerDistance;
    private readonly float tooCloseDistance;

    public StandStilNode(NavMeshAgent agent, float tooCloseDistance)
    {
        this.agent = agent;
        this.tooCloseDistance = tooCloseDistance;
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        agentToPlayerDistance = Vector3.Distance(agent.transform.position, playerPosition);

        if(agentToPlayerDistance <= tooCloseDistance && agent.velocity.magnitude < 0.01f)
        {
            Blackboard.instance.SetIndicatorText("You Got me Cornered! Please Move Away!");
            return true;
        }
        return false;
    }
}
