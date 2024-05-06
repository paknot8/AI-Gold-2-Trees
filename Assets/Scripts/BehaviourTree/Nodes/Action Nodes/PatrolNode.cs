using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrolNode : IBaseNode
{
    private readonly NavMeshAgent agent;
    private Vector3 playerPostion;
    private readonly List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    private readonly float shootingDistance;
    private readonly float pickupDetectionDistance;
    private readonly Item item;

    public PatrolNode(NavMeshAgent agent, List<Transform> waypoints, float shootingDistance, float pickupDetectionDistance, Item item)
    {
        this.agent = agent;
        this.waypoints = waypoints;
        this.shootingDistance = shootingDistance;
        this.pickupDetectionDistance = pickupDetectionDistance;
        this.item = item;
    }

    public virtual bool Update()
    {
        playerPostion = Blackboard.instance.GetPlayerPosition();
        if (waypoints != null &&
            Vector3.Distance(agent.transform.position, playerPostion) > shootingDistance)
        {
            Blackboard.instance.SetIndicatorText("Patrolling...");
            MoveToWaypoint();
            return true;
        }
        return false;
    }

    public void MoveToWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned for patrol!");
            return;
        }

        UpdateAgentColor();

        if (HasReachedWaypoint())
        {
            SetDestinationToNextWaypoint();
        }
    }

    private void UpdateAgentColor()
    {
        Vector3 waypointPosition = waypoints[currentWaypointIndex].position;
        Vector3 directionToWaypoint = (waypointPosition - agent.transform.position).normalized;
        Vector3 agentMovementDirection = agent.velocity.normalized;
        float compareDirection = Vector3.Dot(agentMovementDirection, directionToWaypoint);

        if (compareDirection > 0)
        {
            agent.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private bool HasReachedWaypoint()
    {
        return agent.remainingDistance < 0.5f;
    }

    private void SetDestinationToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
