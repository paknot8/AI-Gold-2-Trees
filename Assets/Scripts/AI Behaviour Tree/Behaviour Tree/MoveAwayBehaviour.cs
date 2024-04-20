using UnityEngine;

public class MoveAwayBehaviour : Node
{
    private Transform aiTransform; // Reference to the AI's transform
    public Transform playerTransform; // Reference to the player's transform
    public float moveSpeed = 3f; // Speed at which the AI moves away from the player
    public float moveAwayDistance = 5f; // Distance at which the AI decides to move away

    public MoveAwayBehaviour(Transform aiTransform)
    {
        this.aiTransform = aiTransform;
    }

    public override bool Execute()
    {
        if (Vector3.Distance(aiTransform.position, playerTransform.position) < moveAwayDistance)
        {
            // Calculate direction away from the player
            Vector3 moveDirection = aiTransform.position - playerTransform.position;
            moveDirection.y = 0; // Keep the AI at the same height

            // Normalize and move away from the player
            aiTransform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
            return true;
        }
        return false;
    }
}
