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
        if (enemyAgent.remainingDistance < 0.5f)
        {
            // Move to the next waypoint
            SetDestinationToNextWaypoint();
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        // Update the current waypoint index
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

        // Set the destination to the next waypoint
        enemyAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public bool Update()
    {
        MoveToWaypoint();
        return true;
    }
}
