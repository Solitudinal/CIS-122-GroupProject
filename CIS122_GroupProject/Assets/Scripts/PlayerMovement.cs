// Niloy Bappy
// Tsimtxuj Her
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float encounterRate = 0.05f;  // 10% chance of encounter per movement
    // This will store whether the player is in an encounter zone
    private bool isInEncounterZone = false;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    // Static variable to store the previous scene name
    public static string previousSceneName;

    //public VectorValue startingPosition;

    /*
    private void Start()
    {
        transform.position = startingPosition.initialValue;
    }
    */
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

            // Check for random encounter only if the player is in an encounter area
            if (isInEncounterZone)
            {
                CheckForRandomEncounter();
            }

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


    private void CheckForRandomEncounter()
    {
        if (Random.value < encounterRate)
        {
            TriggerRandomEncounter();
        }
    }

    private void TriggerRandomEncounter()
    {
        Debug.Log("Random encounter triggered!");
        // Example: BattleManager.Instance.StartBattle();
        // Save the current scene name before switching
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Combat");
    }


    // This method can be called from the Battle Scene to return to the previous scene
    public static void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
    }

    // This method will be called when the player enters a collider with an "EncounterZone" tag
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EncounterZone"))
        {
            isInEncounterZone = true;
        }
    }

    // This method will be called when the player exits the collider with an "EncounterZone" tag
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EncounterZone"))
        {
            isInEncounterZone = false;
        }
    }

    /*
     
     1. Create a 2D collider (e.g., BoxCollider2D) in scene for each encounter area.
     
     2.Set the collider to be a trigger by checking the �Is Trigger� option in the Collider2D component.

      3.Tag the colliders with "EncounterZone".

     */
}