using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public LayerMask Ground, Player;

    // Patrolling
    public Transform[] waypoints; // Array to hold the waypoints
    int waypointIndex;
    Vector2 target;

    // States
    public float sightRange;
    public bool playerInSightRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        UpdateDestination();
    }

    void Update()
    {
        // Check for sight range
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, Player);

        if (!playerInSightRange) 
        {
            UpdateDestination();
            
            if (Vector2.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }

        if (playerInSightRange) ChasePlayer();
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // // Move the enemy towards the waypoint
    // transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, step);

    // // Flip the enemy to face the direction of movement
    // FlipEnemy(direction);


    // void FlipEnemy(Vector3 direction)
    // {
    //     if (direction.magnitude > 0.01f) // If moving
    //         {
    //             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //             transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust the angle if needed
    //         }
    // }
}

