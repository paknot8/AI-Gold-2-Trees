using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrolNode : IBaseNode
{
    private NavMeshAgent enemyAgent;
    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    public PatrolNode(NavMeshAgent agent, List<Transform> patrolWaypoints)
    {
        enemyAgent = agent;
        waypoints = patrolWaypoints;

        // Start patrolling towards the first waypoint
        SetDestinationToNextWaypoint();
    }

    public void MoveToWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned for patrol!");
            return;
        }

        // Check if we have reached the current waypoint
        // Move to the next waypoint
        if (enemyAgent.remainingDistance < 0.5f)
        {
            SetDestinationToNextWaypoint();
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; // Update the current waypoint index
        enemyAgent.SetDestination(waypoints[currentWaypointIndex].position); // Set the destination to the next waypoint
    }

    public bool Update()
    {
        if (waypoints == null)
        {
            Debug.LogError("Waypoints are not assigned!");
            return false;
        }
        else
        {
            MoveToWaypoint();
            return true;
        }
    }

    // Draw gizmos to visualize waypoints in the Unity editor, this will be called automatically
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 1f);
        }
    }
}
