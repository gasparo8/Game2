using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDialogueTrigger : MonoBehaviour
{
    public Dialogue bookDialogue;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void GoGetBookDialogue()
    {
        dialogueManager.StartDialogue(bookDialogue);
    }
}