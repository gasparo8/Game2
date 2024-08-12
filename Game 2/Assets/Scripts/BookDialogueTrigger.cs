using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDialogueTrigger : MonoBehaviour
{
    public Dialogue bookDialogue;
    public Dialogue bookCouchDialogue;
    public Dialogue postReadingCutsceneDialogue;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void GoGetBookDialogue()
    {
        dialogueManager.StartDialogue(bookDialogue);
    }

    public void BookToCouchDialogue()
    {
        dialogueManager.StartDialogue(bookCouchDialogue);
    }

    public void postReadingCutscene()
    {
        dialogueManager.StartDialogue(postReadingCutsceneDialogue);
    }
}