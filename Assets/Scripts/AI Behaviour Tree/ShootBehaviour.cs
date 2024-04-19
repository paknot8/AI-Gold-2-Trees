using UnityEngine;

public class ShootBehaviour : Node
{
    private Transform aiTransform; // Reference to the AI's transform
    public Transform playerTransform; // Reference to the player's transform
    public float shootDistance = 10f; // Distance at which the AI decides to shoot

    public ShootBehaviour(Transform aiTransform)
    {
        this.aiTransform = aiTransform;
    }

    public override bool Execute()
    {
        if (Vector3.Distance(aiTransform.position, playerTransform.position) < shootDistance)
        {
            // Add code here to shoot at the player
            Debug.Log("Shooting at the player!");
            return true;
        }
        return false;
    }
}
