using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PickupNode : IBaseNode
{
    private readonly NavMeshAgent enemy;
    private readonly Transform player;
    private readonly float pickupDetectionDistance;
    private readonly float attackDistance;
    private readonly GameObject item; // Reference to the item GameObject

    public PickupNode(NavMeshAgent enemy, Transform player, float pickupDetectionDistance, float attackDistance, GameObject item)
    {
        this.enemy = enemy;
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
            if (Vector3.Distance(enemy.transform.position, item.transform.position) < pickupDetectionDistance
                && Vector3.Distance(enemy.transform.position, item.transform.position) > attackDistance)
            {
                enemy.SetDestination(item.transform.position);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.LogError("Item GameObject not found in the scene!");
            return false;
        }
    }
}
