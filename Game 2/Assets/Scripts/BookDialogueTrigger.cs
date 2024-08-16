/*using System.Collections;
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
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDialogueTrigger : MonoBehaviour
{
    public Dialogue bookDialogue;
    public Dialogue bookCouchDialogue;
    public Dialogue postReadingCutsceneDialogue;
    public AudioSource doorPoundAudio; // Reference to the door pound audio
    private DialogueManager dialogueManager;
    public float doorPoundDelay = 4f;

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
        StartCoroutine(PlayDoorPoundAfterDelay(doorPoundDelay)); // Wait for the specified delay
    }

    private IEnumerator PlayDoorPoundAfterDelay(float doorPoundDelay)
    {
        yield return new WaitForSeconds(doorPoundDelay);

        // Play the door pound audio
        if (doorPoundAudio != null)
        {
            doorPoundAudio.Play();
        }
    }
}
