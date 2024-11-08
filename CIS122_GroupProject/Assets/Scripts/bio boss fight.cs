using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBattleTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private string previousScene;

    private void Start()
    {
        // Stores the name of the current scene to return to later
        previousScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the player has entered the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player in range. Press 'F' to start battle.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Checks if the player has exited the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player out of range.");
        }
    }

    private void Update()
    {
        // Checks if player is in range and presses 'F'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StartBattle();
        }
    }

    private void StartBattle()
    {
        Debug.Log("Starting battle...");
        SceneManager.LoadScene("Boss Combat"); 
    }

    // This method should be called after the battle ends to return to the previous scene
    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }
}
