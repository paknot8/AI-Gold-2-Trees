using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrolNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    private readonly float distanceFromPlayer;

    public PatrolNode(NavMeshAgent agent, List<Transform> waypoints,Transform player,float distanceFromPlayer)
    {
        this.agent = agent;
        this.waypoints = waypoints;
        this.player = player;
        this.distanceFromPlayer = distanceFromPlayer;
    }

    public virtual bool Update()
    {
        if (waypoints == null)
        {
            Debug.LogError("Waypoints are not assigned!");
            return false;
        }

        // Check if the player is outside moveAwayDistance
        if (PlayerWithinDistance())
        {   
            agent.isStopped = false;
            MoveToWaypoint();
            return true;
        }
        else
        {
            agent.isStopped = true;
            return false;
        }
    }

    private bool PlayerWithinDistance()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
        return distanceToPlayer >= distanceFromPlayer;
    }

    public void MoveToWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned for patrol!");
            return;
        }

        Vector3 waypointPosition = waypoints[currentWaypointIndex].position;        
        Vector3 directionToWaypoint = (waypointPosition - agent.transform.position).normalized; // Calculate the direction towards the current waypoint
        Vector3 agentMovementDirection = agent.velocity.normalized; // Calculate the agent's movement direction 
        float compareDirection = Vector3.Dot(agentMovementDirection, directionToWaypoint); // Check if the agent is moving towards the waypoint by comparing directions

        // Check if we have reached the current waypoint
        // Move to the next waypoint
        if (agent.remainingDistance < 0.5f)
        {
            SetDestinationToNextWaypoint();
        }

        // Agent is moving towards the waypoint
        if (compareDirection > 0)
        {
            agent.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; // Update the current waypoint index
        agent.SetDestination(waypoints[currentWaypointIndex].position); // Set the destination to the next waypoint
    }
}
