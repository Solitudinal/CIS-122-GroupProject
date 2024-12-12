using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Niloy Sarker Bappy

public class Door : MonoBehaviour
{


    private Animator animator;
    private bool playerInRange = false; // Tracks if player is in range

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is in range and presses the "F" key
        if (playerInRange)
        {
            animator.SetBool("Open", true); // Open the chest
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true; // Player is in range
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false; // Player left the range
            animator.SetBool("Open", false); // Close the chest
        }
    }


}
