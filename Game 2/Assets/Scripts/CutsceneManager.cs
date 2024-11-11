using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector pizzaCutsceneDirector;
    public GameObject thePlayer;
    public GameObject playerCamera;
    public GameObject pizzaCam;
    public GameObject chewingCam; // Reference to the new ChewingCam
    public TaskManager taskManager; // Reference to the TaskManager
    public DialogueManager dialogueManager; // Reference to the DialogueManager

    public DoorController frontDoorController; // Reference to the Front Door Controller

    private void Start()
    {
        if (pizzaCam != null)
        {
            pizzaCam.SetActive(false); // Ensure the pizza cam is initially inactive
        }
        if (chewingCam != null)
        {
            chewingCam.SetActive(false); // Ensure the chewing cam is initially inactive
        }


        // Ensure the front door is closed before starting the cutscene
        if (frontDoorController != null)
        {
            frontDoorController.EnsureFrontDoorClosed();
        }

    }

    public void TriggerPizzaEatingCutscene()
    {

        if (pizzaCutsceneDirector != null)
        {
            pizzaCutsceneDirector.Play();
        }

        if (pizzaCam != null)
        {
            pizzaCam.SetActive(true); // Activate the pizza cam
        }
        if (playerCamera != null)
        {
            playerCamera.SetActive(false); // Deactivate the player camera
        }

        StartCoroutine(PlayPizzaCutscene());
    }

    private IEnumerator PlayPizzaCutscene()
    {
        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = false;
        }

        // Wait until the PlayableDirector has finished playing
        while (pizzaCutsceneDirector.state == PlayState.Playing)
        {
            yield return null;
        }

        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = true;
        }
        if (playerCamera != null)
        {
            playerCamera.SetActive(true); // Reactivate the player camera
        }
        if (pizzaCam != null)
        {
            pizzaCam.SetActive(false); // Deactivate the pizza cam
        }
        if (chewingCam != null)
        {
            chewingCam.SetActive(false); // Deactivate the chewing cam
        }


        // Notify TaskManager that the cutscene is complete
        if (taskManager != null)
        {
            taskManager.OnCutsceneComplete();
        }
    }
}