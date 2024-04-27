using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PickupNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly float pickupDetectionDistance;
    private readonly Item item; // Reference to the item GameObject
    private readonly TextMeshProUGUI text;

    public PickupNode(NavMeshAgent agent, float pickupDetectionDistance, Item item, TextMeshProUGUI text)
    {
        this.agent = agent;
        this.pickupDetectionDistance = pickupDetectionDistance;
        this.item = item;
        this.text = text;
    }

    public virtual bool Update()
    {
        // Check if the item GameObject exists
        if (item != null)
        {
            // Check if the item is within pickup detection distance
            if (Vector3.Distance(agent.transform.position, item.transform.position) <= pickupDetectionDistance)
            {
                text.text = "Snif.. Snif... Walking to item...";
                agent.SetDestination(item.transform.position);
                agent.GetComponent<Renderer>().material.color = Color.green;
            }
            return true;
        }
        return false;
    }
}
