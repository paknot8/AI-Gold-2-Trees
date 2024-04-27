using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTalkNode : IBaseNode
{
    private readonly string dialogue;
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly TextMeshProUGUI text;
    private readonly float moveAwayDistance;
    private readonly float attackDistance;

    public EnemyTalkNode(string dialogue, NavMeshAgent agent, Transform player, TextMeshProUGUI text,
        float moveAwayDistance, float attackDistance)
    {
        this.dialogue = dialogue;
        this.agent = agent;
        this.player = player;
        this.text = text;
        this.moveAwayDistance = moveAwayDistance;
        this.attackDistance = attackDistance;
    }

    public virtual bool Update() 
    {
        if(text != null)
        {
            if(Vector3.Distance(player.transform.position, agent.transform.position) < moveAwayDistance)
            {
                text.text = (dialogue);
            }
            else if (Vector3.Distance(player.transform.position, agent.transform.position) > moveAwayDistance
                && Vector3.Distance(player.transform.position, agent.transform.position) < attackDistance)
            {
                text.text = ("I'm Gonna Get Ya!");
            }
            else if (Vector3.Distance(player.transform.position, agent.transform.position) > attackDistance)
            {
                text.text = ("...");
            }
            return true;
        }
        return false;
    }
}
