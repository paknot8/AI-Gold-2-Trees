using UnityEngine;
using UnityEngine.AI;

// For the detection range
public class DetectionNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private float detectionDistance;

    public DetectionNode(NavMeshAgent enemyAgent, Transform playerTransform, float detectionDistance)
    {
        this.enemyAgent = enemyAgent;
        this.playerTransform = playerTransform;
        this.detectionDistance = detectionDistance;
    }

    public virtual bool Update()
    {
        return true;
    }

    private void OnDrawGizmos() // Optional for visualization
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(enemyAgent.transform.position, detectionDistance);
    }
}
