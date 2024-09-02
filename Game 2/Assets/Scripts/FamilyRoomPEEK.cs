using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyRoomPEEK : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public GameObject peekerObject; // Reference to the FrontDoorPeeker GameObject
    private bool animationPlayed = false; // To ensure the animation plays only once
    public ShedNoConcern shedNoConcernScript; // Reference to the ShedNoConcern script

    private DialogueManager dialogueManager;
    public Dialogue postFamilyRoomPeekDialogue;
    private int postFamilyRoomPeekDialogueDelay = 8;

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (peekerObject != null)
        {
            peekerObject.SetActive(false); // Ensure the friend object is disabled at the start
        }

        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ShedNoConcern trigger has been activated before playing the animation
        if (other.CompareTag("Player") && !animationPlayed && shedNoConcernScript.hasTriggered)
        {
            peekerObject.SetActive(true);
            // Set the trigger to play the animation
            anim.SetTrigger("PlayFamilyRoomPeek");
            animationPlayed = true; // Ensure it only plays once
            StartCoroutine(PostFamilyRoomPeekDialogue());
        }
    }

    private IEnumerator PostFamilyRoomPeekDialogue()
    {
        // Wait for 6 seconds before starting the dialogue
        yield return new WaitForSeconds(postFamilyRoomPeekDialogueDelay);

        if (animationPlayed)
        {
            dialogueManager.StartDialogue(postFamilyRoomPeekDialogue);
        }
    }
}
