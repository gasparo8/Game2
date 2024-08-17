using System.Collections;
using UnityEngine;

public class BookDialogueTrigger : MonoBehaviour
{
    public Dialogue bookDialogue;
    public Dialogue bookCouchDialogue;
    public Dialogue postReadingCutsceneDialogue;

    public AudioSource doorPoundAudio;  // Reference to the door pound audio
    public AudioSource heartbeatAudio;  // Reference to the heartbeat audio
    public DogsBarking dogsBarkingScript;  // Reference to the DogsBarking script

    private DialogueManager dialogueManager;
    public float doorPoundDelay = 4f;
    public float postHeartbeatDelay = 15f;  // Delay after the heartbeat before triggering the dogs barking

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

    public void PostReadingCutscene()
    {
        dialogueManager.StartDialogue(postReadingCutsceneDialogue);
        StartCoroutine(PlaySoundsAfterDelay(doorPoundDelay));  // Wait for the specified delay
    }

    private IEnumerator PlaySoundsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the door pound audio
        if (doorPoundAudio != null)
        {
            doorPoundAudio.Play();
        }

        // Play the heartbeat audio and wait for it to finish
        if (heartbeatAudio != null)
        {
            heartbeatAudio.Play();
            yield return new WaitForSeconds(heartbeatAudio.clip.length);
        }

        // Wait an additional 15 seconds after the heartbeat ends
        yield return new WaitForSeconds(postHeartbeatDelay);

        // Trigger the DogsBarking script
        if (dogsBarkingScript != null)
        {
            dogsBarkingScript.StartBarking();  // Ensure DogsBarking has a method to start the barking
        }
    }
}
