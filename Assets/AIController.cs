using UnityEngine;

public enum AIState
{
    Idle,
    Walking
}

public class AIController : MonoBehaviour
{
    public float walkSpeed = 3f;

    private AIState currentState = AIState.Idle;
    private Transform target; // This could be the player or a destination point

    void Update()
    {
        // Check current state and perform actions accordingly
        switch (currentState)
        {
            case AIState.Idle:
                // Perform idle actions/animations
                // Example: Play idle animations
                break;

            case AIState.Walking:
                // Move towards the target
                if (target != null)
                {
                    MoveTowardsTarget();
                }
                break;

            default:
                break;
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the AI towards the target
        transform.position += direction * walkSpeed * Time.deltaTime;

        // Rotate towards the target (optional)
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Method to set the AI state to walking
    public void SetWalkingState(Transform newTarget)
    {
        currentState = AIState.Walking;
        target = newTarget;
    }

    // Method to set the AI state to idle
    public void SetIdleState()
    {
        currentState = AIState.Idle;
    }
}
