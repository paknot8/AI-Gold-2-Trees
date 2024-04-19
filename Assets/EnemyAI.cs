using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    
    private Transform playerTransform;

    private ChaseBehaviour chaseBehaviour;
    private ShootBehaviour shootBehaviour;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        chaseBehaviour = new ChaseBehaviour(transform); // Passes transform
        shootBehaviour = new ShootBehaviour(transform); // Passes transform
    }

    private void Update()
    {
        if (IsPlayerWithinChasingRange() && chaseBehaviour.Execute()) // Checks if player is within chasing range before executing chase behavior
        {
            // Chase behaviour executed
        }
        else if (IsPlayerWithinShootingRange() && shootBehaviour.Execute()) // Checks if player is within shooting range before executing shoot behavior
        {
            // Shoot behaviour executed
        }
        else
        {
            // Handle other behaviors or default actions
        }
    }

    public bool IsPlayerWithinChasingRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= chasingRange;
    }

    public bool IsPlayerWithinShootingRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= shootingRange;
    }
}
