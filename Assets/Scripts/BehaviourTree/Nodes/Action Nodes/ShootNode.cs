using UnityEngine;
using UnityEngine.AI;

public class ShootNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly GameObject bulletPrefab;

    private readonly float moveAwayDistance;
    private readonly float shootingDistance;
    private readonly float shootInterval = 1f;
    private readonly float bulletSpeed = 10f;
    private readonly float bulletLifetime = 3f; 
    private float lastShotTime = 0f;
    

    public ShootNode(NavMeshAgent agent, Transform player, GameObject bulletPrefab, float shootingDistance, float moveAwayDistance)
    {
        this.agent = agent;
        this.player = player;
        this.bulletPrefab = bulletPrefab;
        this.shootingDistance = shootingDistance;
       this.moveAwayDistance = moveAwayDistance;
    }

    public virtual bool Update()
    {       
        if (Time.time >= lastShotTime + shootInterval 
            && Vector3.Distance(agent.transform.position, player.position) < shootingDistance
            && Vector3.Distance(agent.transform.position, player.position) > moveAwayDistance)
        {
            agent.transform.LookAt(player.position);
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
            return true;
        }
        else
        {

            return false;
        }
    }

    private void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, agent.transform.position, Quaternion.identity);
        Vector3 direction = (player.position - agent.transform.position).normalized; // Calculate direction towards the player
        bullet.transform.rotation = Quaternion.LookRotation(direction); // Rotate the bullet to face the shooting direction
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        GameObject.Destroy(bullet, bulletLifetime);
    }
}
