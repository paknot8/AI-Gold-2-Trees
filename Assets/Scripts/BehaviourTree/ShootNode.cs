using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private GameObject bulletPrefab;

    private float shootInterval = 1f; // Time interval between each shot
    private float bulletSpeed = 10f; // Speed of the bullet
    private float bulletLifetime = 3f; // Lifetime of the bullet
    private float shootingDistance; // Shooting distance
    private float lastShotTime = 0f;

    public ShootNode(NavMeshAgent enemyAgent, Transform playerTransform, GameObject bulletPrefab, float shootingDistance)
    {
        this.enemyAgent = enemyAgent;
        this.playerTransform = playerTransform;
        this.bulletPrefab = bulletPrefab;
        this.shootingDistance = shootingDistance;
    }

    public virtual bool Update()
    {       
        // Check if player is within shooting distance and shoot interval is reached
        if (Time.time >= lastShotTime + shootInterval && Vector3.Distance(enemyAgent.transform.position, playerTransform.position) <= shootingDistance)
        {
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
            return true;
        }
        return false;
    }

    private void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, enemyAgent.transform.position, Quaternion.identity);
        Vector3 direction = (playerTransform.position - enemyAgent.transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        GameObject.Destroy(bullet, bulletLifetime);
    }

    // Draw gizmo to visualize the shooting distance
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(enemyAgent.transform.position, shootingDistance);
    }
}
