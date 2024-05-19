using UnityEngine;
using UnityEngine.AI;

public class ShootNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly GameObject bulletPrefab;
    private GameObject bullet;
    private Vector3 playerPosition;
    private Vector3 direction;

    // --- Distances ---
    private readonly float moveAwayDistance;
    private readonly float shootingDistance;

    // --- Shooting and Bullet ---
    private readonly float shootInterval = 1f;
    private readonly float bulletSpeed = 20f;
    private readonly float bulletLifetime = 3f;
    private float lastShotTime = 0f;

    public ShootNode(NavMeshAgent agent, GameObject bulletPrefab, float shootingDistance, float moveAwayDistance)
    {
        this.agent = agent;
        this.bulletPrefab = bulletPrefab;
        this.shootingDistance = shootingDistance;
        this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {   
        playerPosition = Blackboard.instance.GetPlayerPosition();
        if (Time.time >= lastShotTime + shootInterval 
            && Vector3.Distance(agent.transform.position, playerPosition) < shootingDistance
            && Vector3.Distance(agent.transform.position, playerPosition) > moveAwayDistance)
        {
            agent.transform.LookAt(playerPosition);
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
        }
        return true;
    }

    private void Shoot()
    {
        bullet = GameObject.Instantiate(bulletPrefab, agent.transform.position, Quaternion.identity);
        direction = (playerPosition - agent.transform.position).normalized; // Calculate direction towards the player
        bullet.transform.rotation = Quaternion.LookRotation(direction); // Rotate the bullet to face the shooting direction
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        GameObject.Destroy(bullet, bulletLifetime);
    }
}
