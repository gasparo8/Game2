using System.Collections;
using UnityEngine;
using UnityEngine.Playables; // Required for Timeline

public class OfficeCutsceneManager : MonoBehaviour
{
    // Reference to the Player Camera and Office Camera
    public GameObject playerCamera;
    public GameObject officeCamera;

    // Reference to the PlayableDirector for the cutscene timeline
    public PlayableDirector cutsceneTimeline;

    void Start()
    {
        // Ensure the office camera is initially off
        officeCamera.SetActive(false);
    }

    // Method to start the cutscene
    public void StartCutscene()
    {
        // Deactivate the player camera and activate the office camera
        playerCamera.SetActive(false);
        officeCamera.SetActive(true);

        // Play the timeline cutscene
        cutsceneTimeline.Play();

        // Subscribe to the event that triggers when the cutscene ends
        cutsceneTimeline.stopped += OnCutsceneEnd;
    }

    // Method called when the cutscene ends
    private void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == cutsceneTimeline)
        {
            // Reactivate the player camera and deactivate the office camera
            playerCamera.SetActive(true);
            officeCamera.SetActive(false);

            // Unsubscribe from the event to avoid memory leaks
            cutsceneTimeline.stopped -= OnCutsceneEnd;

            Debug.Log("Cutscene ended, player camera reactivated.");
        }
    }
}
