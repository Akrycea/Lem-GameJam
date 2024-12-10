using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 movement;
    bool isMovingRight;

    [SerializeField] private Transform visuals;
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 movmentVector = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        // animator.Play("");
        //}

        if (Input.GetKey(KeyCode.S))
        {

            animator.SetBool("isForward", true);
            animator.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("isRight", true);
            animator.SetBool("isForward", false);
        }
        else
        {
            animator.SetBool("isRight", false);
            animator.SetBool("isForward", false);
        }


            if (movement.x > 0f)
            {

                visuals.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                visuals.localScale = new Vector3(-1f, 1f, 1f);

            }

        rb.MovePosition(movmentVector); 
    }
}
