using UnityEngine;
using UnityEngine.AI;

// For the detection range
public class DetectionNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private float maxDetectionRange;

    public DetectionNode(NavMeshAgent enemyAgent, Transform playerTransform, float detectionDistance)
    {
        this.enemyAgent = enemyAgent;
        this.playerTransform = playerTransform;
        this.maxDetectionRange = detectionDistance;
    }
    // This is just for initiliazing the dectection range maximum
    public virtual bool Update()
    {
        return true;
    }
}
