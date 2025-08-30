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
    public GameObject lightOFFLookCamera;
    public GameObject couchWalkPoint;
    public LightSwitch lightSwitch; // Reference to the LightSwitch script

    public Light lightToTurnOff; // Assign the specific light in the Inspector
    public Renderer lightRenderer; // Renderer for the light's material
    public Material offMaterial; // Material to use when the light is off
    public AudioSource audioSource; // AudioSource for sound effects
    public AudioClip switchOffClip; // Sound for turning the light off

    public Renderer[] lightRenderers; // Array of renderers for each light
    public Material[] offMaterials; // Array of materials to use when the lights are off

    public Dialogue postSecondReadingCutsceneDialogue;

    // New: Reference to PowerOutageScript
    public PowerOutageScript powerOutageScript;
    public bool cutsceneCompleted = false; // Track if the cutscene is completed

    private DialogueManager dialogueManager;

    public FamilyRoomPEEK familyRoomPeek;

    // Start is called before the first frame update
    void Start()
    {
        if (readingCamera != null)
        {
            readingCamera.SetActive(false); // Ensure the reading cam is initially inactive
        }

        if (lightOFFLookCamera != null)
        {
            lightOFFLookCamera.SetActive(false); // Ensure the lightOFFLookCamera cam is initially inactive
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

        // Ensure dialogueManager is assigned properly
        dialogueManager = FindObjectOfType<DialogueManager>();
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

        if (lightOFFLookCamera != null)
        {
            lightOFFLookCamera.SetActive(true); // Enable the lightOFFLookCamera camera
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

    public void TurnOffLightDuringCutscene()
    {
        // Turn off the light
        lightToTurnOff.enabled = false;

        // Change the light's materials to offMaterials
        for (int i = 0; i < lightRenderers.Length; i++)
        {
            if (lightRenderers[i] != null && offMaterials[i] != null)
            {
                lightRenderers[i].material = offMaterials[i]; // Set the appropriate off material for each renderer
            }
        }

        // Play the switch off sound if assigned
        if (audioSource != null && switchOffClip != null)
        {
            audioSource.PlayOneShot(switchOffClip);
        }
    }

    // This method is called when the cutscene finishes
    public void OnCutsceneFinished(PlayableDirector director)
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

            if (lightOFFLookCamera != null)
            {
                lightOFFLookCamera.SetActive(false); // Disable the reading camera
            }

            cutsceneCompleted = true; // Mark the cutscene as completed

            // Trigger the post-cutscene dialogue
            PostSecondReadingCutsceneDialogue();

            if (powerOutageScript != null)
                powerOutageScript.StartPowerOutageCoroutine(); // Notify power outage script

            Debug.Log("Cutscene finished. Starting power outage coroutine.");
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
        public void PostSecondReadingCutsceneDialogue()
        {
            dialogueManager.StartDialogue(postSecondReadingCutsceneDialogue);
        }
    }
