using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    public bool IsPlayerWithinChasingRange(Transform playerTransform)
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= chasingRange;
    }

    public bool IsPlayerWithinShootingRange(Transform playerTransform)
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= shootingRange;
    }
}
