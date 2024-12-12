using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



//written by Niloy Sarker Bappy

public class CSE_CameraPan : CutsceneElementBase
{

    private CinemachineVirtualCamera vCam;
    [SerializeField] private Vector2 distanceToMove;
    [SerializeField] private Transform characterTransform; // Reference to the character to follow after the pan
    [SerializeField] private float movementThreshold = 0.1f; // Minimum movement to detect


    private Vector3 lastCharacterPosition;
    private bool isPlayerMoving;

    public override void Execute()
    {

        vCam = cutsceneHandler.vCam;

        vCam.Follow = null;

        // Store the last known position of the character
        lastCharacterPosition = characterTransform.position;

        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalPosition = vCam.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(distanceToMove.x, distanceToMove.y, 0);

        float startTime = Time.time;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            vCam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;

            yield return null;

        }

        vCam.transform.position = targetPosition; // Ensure the camera reaches the exact target position

        // Signal the cutscene handler to proceed to the next element
        cutsceneHandler.PlayNextElement();


        // Wait for the player to start moving before resuming camera follow
        yield return StartCoroutine(WaitForPlayerMovement());

    }

    private IEnumerator WaitForPlayerMovement()
    {
        while (true)
        {
            // Check if the player has moved beyond the movement threshold
            if (Vector3.Distance(lastCharacterPosition, characterTransform.position) > movementThreshold)
            {
                // Resume following the character
                vCam.Follow = characterTransform;
                yield break; // Exit the coroutine
            }

            // Update the last known character position
            lastCharacterPosition = characterTransform.position;

            yield return null; // Wait for the next frame
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
