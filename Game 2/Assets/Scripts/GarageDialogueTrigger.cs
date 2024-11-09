using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueManager != null) // Null check to avoid errors
            {
                dialogueManager.StartDialogue(dialogue);
            }
        }
    }
}
