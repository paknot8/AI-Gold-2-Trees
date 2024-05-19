using UnityEngine;
using UnityEngine.AI;

public class PickupNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPosition;
    private readonly Item item; // Reference to the item GameObject

    // --- Distances ---
    private readonly float pickupDetectionDistance;

    public PickupNode(NavMeshAgent agent, float pickupDetectionDistance, Item item)
    {
        this.agent = agent;
        this.pickupDetectionDistance = pickupDetectionDistance;
        this.item = item;
    }

    public virtual bool Update()
    {
        playerPosition = Blackboard.instance.GetPlayerPosition();
        if (item != null)
        {
            if(Vector3.Distance(agent.transform.position, playerPosition) <= pickupDetectionDistance)
            {
                return false;
            }
            // Check if the item is within pickup detection distance
            if (Vector3.Distance(agent.transform.position, item.transform.position) <= pickupDetectionDistance)
            {
                Blackboard.instance.SetIndicatorText("Goint to Pickup Item...");
                agent.SetDestination(item.transform.position);
                agent.GetComponent<Renderer>().material.color = Color.green;
                return true;
            }
        }
        return false;
    }
}
