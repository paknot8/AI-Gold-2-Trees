using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTalkNode : IBaseNode
{
    private readonly string dialogue;
    private readonly NavMeshAgent agent;
    private readonly TextMeshProUGUI text;
    private readonly float moveAwayDistance;
    private readonly float attackDistance;

    public EnemyTalkNode(string dialogue, NavMeshAgent agent, TextMeshProUGUI text,
        float moveAwayDistance, float attackDistance)
    {
        this.dialogue = dialogue;
        this.agent = agent;
        this.text = text;
        this.moveAwayDistance = moveAwayDistance;
        this.attackDistance = attackDistance;
    }

    public virtual bool Update() 
    {
        Vector3 playerPosition = Blackboard.instance.GetPlayerPosition();
        if(text != null)
        {
            if(Vector3.Distance(playerPosition, agent.transform.position) < moveAwayDistance)
            {
                text.text = (dialogue);
            }
            else if (Vector3.Distance(playerPosition, agent.transform.position) > moveAwayDistance
                && Vector3.Distance(playerPosition, agent.transform.position) < attackDistance)
            {
                text.text = ("I'm Gonna Get Ya!");
            }
            else if (Vector3.Distance(playerPosition, agent.transform.position) > attackDistance)
            {
                text.text = ("Time to Patrol...");
            }
            return true;
        }
        return false;
    }
}
