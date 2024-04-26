using UnityEngine;
using UnityEngine.AI;

// For the detection range
public class DetectionNode : IBaseNode
{
    private readonly NavMeshAgent enemyAgent;
    private readonly Transform playerTransform;
    private readonly float maxDetectionRange;

    public DetectionNode(NavMeshAgent enemyAgent, Transform playerTransform, float maxDetectionRange)
    {
        this.enemyAgent = enemyAgent;
        this.playerTransform = playerTransform;
        this.maxDetectionRange = maxDetectionRange;
    }

    public virtual bool Update()
    {
        // Check if the distance between enemy and player is within the maximum detection range
        float distanceToPlayer = Vector3.Distance(enemyAgent.transform.position, playerTransform.position);
        bool playerWithinDetectionRange = distanceToPlayer <= maxDetectionRange;

        if (playerWithinDetectionRange)
        {
            // Player is within detection range
            Debug.Log("Player within max range of " + maxDetectionRange);
        }
        else
        {
            // Player is outside detection range
            Debug.Log("Player outside range");
        }

        // Always return true for now, as this is just for initialization
        return true;
    }
}
