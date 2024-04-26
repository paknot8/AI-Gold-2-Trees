using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject floor; // Reference to the floor where the item can respawn

    void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with an object tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Respawn the item randomly on the floor
            RespawnItem();
            Destroy(gameObject); // Destroy the item
        }
    }

    void RespawnItem()
    {
        // Get the floor bounds
        Renderer floorRenderer = floor.GetComponent<Renderer>();
        float minX = floorRenderer.bounds.min.x;
        float maxX = floorRenderer.bounds.max.x;
        float minZ = floorRenderer.bounds.min.z;
        float maxZ = floorRenderer.bounds.max.z;

        // Generate random respawn position within the floor bounds
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 respawnPosition = new Vector3(randomX, transform.position.y, randomZ);

        // Respawn the item at the random position
        Instantiate(gameObject, respawnPosition, Quaternion.identity);
    }
}
