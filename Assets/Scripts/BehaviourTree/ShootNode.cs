using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private Transform playerTransform;
    private GameObject bulletPrefab;

    private readonly float shootInterval = 1f;
    private readonly float bulletSpeed = 10f;
    private readonly float bulletLifetime = 3f; 
    private readonly float shootingDistance;
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
        if (Time.time >= lastShotTime + shootInterval 
        && Vector3.Distance(enemyAgent.transform.position, playerTransform.position) <= shootingDistance)
        {
            enemyAgent.isStopped = true;
            enemyAgent.transform.LookAt(playerTransform.position);
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
            return true;
        } 
        else
        {
            enemyAgent.isStopped = false;
            return false;
        }
    }

    private void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, enemyAgent.transform.position, Quaternion.identity);
        // Calculate direction towards the player
        Vector3 direction = (playerTransform.position - enemyAgent.transform.position).normalized;
        // Rotate the bullet to face the shooting direction
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        GameObject.Destroy(bullet, bulletLifetime);
    }
}
