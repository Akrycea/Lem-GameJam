using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] MaskSlider script;

    Animator animator;
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
        animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        UpdateDestination();
    }

    void Update()
    {
        // Check for sight range
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, Player);

        if (!playerInSightRange || script.maskOn == true) 
        {
            UpdateDestination();

            if (Vector2.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }

        if (playerInSightRange && script.maskOn == false) ChasePlayer();

        // if (player.transform.position > agent.transform.position || target.transform.position > agent.transform.position) agent.flipX = true;
        // else agent.flipX = false;
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
}

