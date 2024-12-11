using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{


    private Animator animator;
    private bool playerInRange = false; // Tracks if player is in range

    [SerializeField] AudioSource openChestSound;

    [SerializeField] private TMP_Text rewardText; 
    [SerializeField] private GameObject rewardTextBox; 
    [SerializeField] private int lettersPerSecond = 15;

    private bool isChestOpened = false;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        rewardTextBox.SetActive(false); // Ensure reward text is hidden initially
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !isChestOpened)
        {
            isChestOpened = true; // Prevent re-opening the chest
            animator.SetBool("Open", true);

            if (openChestSound != null)
            {
                openChestSound.Play();
            }

            // TODO: Randomize chest rewards
            StartCoroutine(DisplayReward("You found an energy drink!"));
        }
    }

    // Coroutine that handles item text pop-up
    IEnumerator DisplayReward(string message)
    {
        rewardTextBox.SetActive(true); 
        rewardText.text = ""; 

        foreach (var letter in message)
        {
            rewardText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(2f); 
        rewardTextBox.SetActive(false); 
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
