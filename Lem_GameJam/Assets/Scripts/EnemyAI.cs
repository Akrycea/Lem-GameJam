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
    public Transform[] waypoints; 
    int waypointIndex;
    Vector2 target;

    // States
    public float sightRange;
    public bool playerInSightRange;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateDestination();
    }

    void Update()
    {
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

        if (playerInSightRange && script.maskOn == false) 
        {
            ChasePlayer();
        }

        FlipSprite();
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

    void FlipSprite()
    {
        if (agent.velocity.x < 0 && spriteRenderer.flipX) 
        {
            spriteRenderer.flipX = false;
        }
        
        else if (agent.velocity.x > 0 && !spriteRenderer.flipX) 
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Attack");
        }
    }
}


