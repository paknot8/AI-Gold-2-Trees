using UnityEngine;
using UnityEngine.AI;

public class PickupNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly float pickupDetectionDistance;
    private readonly float attackDistance;
    private readonly Item item; // Reference to the item GameObject

    public PickupNode(NavMeshAgent agent, Transform player, float pickupDetectionDistance, float attackDistance, Item item)
    {
        this.agent = agent;
        this.player = player;
        this.pickupDetectionDistance = pickupDetectionDistance;
        this.attackDistance = attackDistance;
        this.item = item;
    }

    public virtual bool Update()
    {
        // Check if the item GameObject exists
        if (item != null)
        {
            // Check if the item is within pickup detection distance
            if (Vector3.Distance(agent.transform.position, item.transform.position) <= pickupDetectionDistance)
            {
                agent.SetDestination(item.transform.position);
                agent.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        return true;
    }
}
