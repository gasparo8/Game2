using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    private bool animationCooldown = false;

    [SerializeField] private LockController lockController; // Reference to the LockController
    [SerializeField] private DialogueManager dialogueManager; // Reference to the DialogueManager
    [SerializeField] private Dialogue lockedDialogue; // Dialogue for the locked door message
    [SerializeField] private bool isFrontDoor = false; // Flag to identify the front door

    [SerializeField] private AudioSource doorAudioSource; // Reference to the door's AudioSource

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!animationCooldown && (lockController == null || !lockController.IsLocked()))
        {
            if (!doorOpen)
            {
                doorAnim.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
            }
            else
            {
                doorAnim.Play("DoorClose", 0, 0.0f);
                doorOpen = false;
            }

            // Start the cooldown timer
            StartCoroutine(AnimationCooldownTimer());
        }
        else if (lockController != null && lockController.IsLocked())
        {
            Debug.Log("The door is locked and cannot be opened.");
            dialogueManager.StartDialogue(lockedDialogue); // Trigger the locked door dialogue
        }
    }

    private IEnumerator AnimationCooldownTimer()
    {
        // Wait for 1 second before allowing the animation again
        animationCooldown = true;
        yield return new WaitForSeconds(1.0f);
        animationCooldown = false;
    }

    // Call this method when you need to ensure the front door is closed without playing sound
    public void EnsureFrontDoorClosed()
    {
        if (isFrontDoor && doorOpen)
        {
            // Temporarily mute the door's sound
            if (doorAudioSource != null)
            {
                doorAudioSource.mute = true;
            }

            // Close the front door if it is open
            doorAnim.Play("DoorClose", 0, 0.0f);
            doorOpen = false;

            // Wait for 2 seconds and then unmute the sound
            StartCoroutine(UnmuteAfterDelay(2.0f));
        }
    }

    private IEnumerator UnmuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (doorAudioSource != null)
        {
            doorAudioSource.mute = false; // Unmute the sound after the delay
        }
    }
}