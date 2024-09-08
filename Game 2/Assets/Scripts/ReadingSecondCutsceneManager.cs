using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReadingSecondCutsceneManager : MonoBehaviour
{
    public PlayableDirector readingSecondCutsceneDirector;
    public GameObject thePlayer;
    public GameObject playerCam;
    public GameObject openBook;
    public GameObject readingCamera;
    public GameObject couchWalkPoint;

    private bool couchWalkPointTriggered = false; // To prevent retriggering

    // Start is called before the first frame update
    void Start()
    {
        if (readingCamera != null)
        {
            readingCamera.SetActive(false); // Ensure the reading cam is initially inactive
        }

        if (couchWalkPoint != null)
        {
            couchWalkPoint.SetActive(false); // Ensure the couch walk point is initially inactive
        }

        if (readingSecondCutsceneDirector != null)
        {
            // Subscribe to the event that triggers when the cutscene ends
            readingSecondCutsceneDirector.stopped += OnCutsceneFinished;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player
        if (other.CompareTag("Player") && !couchWalkPointTriggered)
        {
            couchWalkPointTriggered = true;
            couchWalkPoint.SetActive(true); // Activate the couch walk point when player enters trigger
        }
    }

    public void TriggerSecondReadingCutscene()
    {
        if (readingSecondCutsceneDirector != null)
        {
            openBook.SetActive(true); // Activate the book object
            readingSecondCutsceneDirector.Play(); // Play the second reading cutscene
            Debug.Log("Second Reading Cutscene Played");
        }

        if (readingCamera != null)
        {
            readingCamera.SetActive(true); // Enable the reading camera
        }

        if (playerCam != null)
        {
            playerCam.SetActive(false); // Disable the player camera
        }

        // Optionally disable couchWalkPoint if not needed after the cutscene
        if (couchWalkPoint != null)
        {
            couchWalkPoint.SetActive(false);
        }
    }

    // This method is called when the cutscene finishes
    private void OnCutsceneFinished(PlayableDirector director)
    {
        // Re-enable the player and player camera after the cutscene ends
        if (director == readingSecondCutsceneDirector)
        {
            if (playerCam != null)
            {
                playerCam.SetActive(true); // Re-enable the player camera
            }

            if (thePlayer != null)
            {
                thePlayer.SetActive(true); // Re-enable the player if necessary
            }

            if (readingCamera != null)
            {
                readingCamera.SetActive(false); // Disable the reading camera
            }

            Debug.Log("Second Reading Cutscene Finished");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to prevent memory leaks
        if (readingSecondCutsceneDirector != null)
        {
            readingSecondCutsceneDirector.stopped -= OnCutsceneFinished;
        }
    }
}
