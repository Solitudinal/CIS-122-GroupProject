using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Xinput", movement.x);
            animator.SetFloat("Yinput", movement.y);
            animator.SetBool("IsWalking", true);

        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
            
    }

    private void FixedUpdate()
    {
        // option 1 - stiff movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // option 2 for movement
        /*
        if (movement.x != 0 || movement.y != 0)
        {
            rb.velocity = movement * speed;
        }
        */

        // option 3 for movement
        //rb.AddForce(movement * speed);
    }
}