using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class FriendJumpScare : MonoBehaviour
{
    public PlayableDirector jumpScareCutscene; // Reference to the PlayableDirector for the cutscene
    public GameObject player; // Reference to the player
    public GameObject jumpTrigger; // Reference to the jump trigger collider
    public LightFlicker lightsFlickerScript; // Reference to the LightFlicker script
    public GameObject neighbor; // Reference to the friend object
    public ShedTrigger shedTriggerScript; // Reference to the ShedTrigger script
    public DialogueTrigger dialogueTrigger; // Reference to the DialogueTrigger script

    private PlayerMovement playerMovement; // Reference to the player's movement script
    public bool jumpScareTriggered = false; // Prevent multiple triggers

    public PianoShedTaskAudio pianoShedTaskAudio;
    private void Start()
    {
        if (jumpTrigger != null)
        {
            jumpTrigger.SetActive(false); // Ensure the jump trigger is disabled at the start
        }

        if (neighbor != null)
        {
            neighbor.SetActive(false); // Ensure the friend object is disabled at the start
        }

        // Get the player's movement script
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        // Check if light flickering has started and enable the jump trigger
        if (lightsFlickerScript != null && jumpTrigger != null && !jumpScareTriggered)
        {
            if (lightsFlickerScript.IsFlickering())
            {
                jumpTrigger.SetActive(true); // Enable the jump trigger when flickering starts
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jumpScareTriggered)
        {
            jumpScareTriggered = true; // Prevent further triggers
            StartCoroutine(JumpScareSequence());
        }
    }

    public IEnumerator JumpScareSequence()
    {
        if (playerMovement != null)
        {
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
        }

        // Activate the friend object before the cutscene starts
        if (neighbor != null)
        {
            neighbor.SetActive(true);
        }

        // Play the jump scare cutscene
        if (jumpScareCutscene != null)
        {
            jumpScareCutscene.Play();

            // Wait until the PlayableDirector has finished playing
            while (jumpScareCutscene.state == PlayState.Playing)
            {
                yield return null;
            }
        }

        // Destroy the jump trigger immediately after it's triggered
        if (jumpTrigger != null)
        {
            Destroy(jumpTrigger); // Destroy the jump trigger
        }

        // Set the shed trigger object active after the friend is destroyed
        if (shedTriggerScript != null && shedTriggerScript.triggerObject != null)
        {
            shedTriggerScript.triggerObject.SetActive(true);
        }

        // Trigger the dialogue after the cutscene ends
        if (dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }

        // Re-enable player movement after cutscene completes
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            pianoShedTaskAudio.PlayAudio();
        }
    }
}
