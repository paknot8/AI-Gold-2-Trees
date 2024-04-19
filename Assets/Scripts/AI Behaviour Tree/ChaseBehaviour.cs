using UnityEngine;

public class ChaseBehaviour : Node
{
    public Transform playerTransform; // Reference to the player's transform
    public float chaseSpeed = 5f; // Speed at which the AI chases the player
    private GameObject aiGameObject; // Reference to the AI GameObject

    public ChaseBehaviour(GameObject aiGameObject)
    {
        this.aiGameObject = aiGameObject;
    }

    public override bool Execute()
    {
        // Calculate direction towards the player
        Vector3 chaseDirection = playerTransform.position - aiGameObject.transform.position;
        chaseDirection.y = 0; // Keep the AI at the same height

        // Normalize and move towards the player
        aiGameObject.transform.position += chaseDirection.normalized * chaseSpeed * Time.deltaTime;
        return true;
    }
}
