using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopTaskManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;
    public BoxCollider mopCollider; // Reference to the mop's BoxCollider
    public GameObject instructionTrigger; // The extra object to activate

    private bool hasActivated = false; // Prevents re-triggering

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (mopCollider != null)
        {
            mopCollider.enabled = false; // Make sure it's off at start
        }

        if (instructionTrigger != null)
        {
            instructionTrigger.SetActive(false); // Ensure it's off at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(dialogue);
            }

            if (mopCollider != null)
            {
                mopCollider.enabled = true; // Activate mop's BoxCollider
            }

            if (instructionTrigger != null)
            {
                instructionTrigger.SetActive(true); // Activate extra instruction object
            }

            hasActivated = true; // Ensures this only happens once
        }
    }
}
