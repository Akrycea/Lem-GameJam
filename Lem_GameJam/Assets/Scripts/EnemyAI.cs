using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] PlayerBehaviour mask;

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

    public AudioSource walk;
    public AudioSource attack;

    void Start()
    {
        walk.Play();

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

        if (!playerInSightRange || mask.isMasked == true) 
        {
            UpdateDestination();

            if (Vector2.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }

        if (playerInSightRange && mask.isMasked == false) 
        {
            ChasePlayer();
        }

        FlipSprite();

        if (mask.hasWon == true || mask.hasDied == true)
        {
            walk.Stop();
        }
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
        if (other.tag == "Player" && mask.isMasked == false)
        {
            walk.Stop();
            attack.Play();
            animator.SetTrigger("Attack");
        }
    }
}


