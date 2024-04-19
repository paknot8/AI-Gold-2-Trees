using UnityEngine;

public class ChaseBehaviour : Node
{
    private Transform enemyTransform;
    private Transform playerTransform;
    private float moveSpeed = 3f; // Adjust this speed as needed

    public ChaseBehaviour(Transform enemyTransform, Transform playerTransform)
    {
        this.enemyTransform = enemyTransform;
        this.playerTransform = playerTransform;
    }

    public override bool Execute()
    {
        // Calculate direction towards the player
        Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;

        // Move the enemy towards the player
        enemyTransform.position += direction * moveSpeed * Time.deltaTime;

        // Optionally, rotate the enemy to face the player
        enemyTransform.rotation = Quaternion.LookRotation(direction);

        return true; // Return true to indicate that the chase behavior was executed
    }
}
