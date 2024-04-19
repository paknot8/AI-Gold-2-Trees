using UnityEngine;

public class PatrolBehaviour : Node
{
    private Transform aiTransform; // Reference to the AI's transform
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex = 0; // Index of the current patrol point

    public float patrolSpeed = 2f; // Speed at which the AI patrols

    public PatrolBehaviour(Transform aiTransform)
    {
        this.aiTransform = aiTransform;
    }

    public override bool Execute()
    {
        Patrol();
        return true;
    }

    void Patrol()
    {
        // Move towards the current patrol point
        aiTransform.position = Vector3.MoveTowards(aiTransform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);

        // Check if reached the current patrol point
        if (Vector3.Distance(aiTransform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}
