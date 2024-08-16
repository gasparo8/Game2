using System.Collections;
using UnityEngine;

public class BookDialogueTrigger : MonoBehaviour
{
    public Dialogue bookDialogue;
    public Dialogue bookCouchDialogue;
    public Dialogue postReadingCutsceneDialogue;

    public AudioSource doorPoundAudio; // Reference to the door pound audio
    public AudioSource heartbeatAudio; // Reference to the heartbeat audio

    private DialogueManager dialogueManager;
    public float doorPoundDelay = 4f;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        // Check if DialogueManager was found
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
        }
        
        // Check if AudioSources are assigned
        if (doorPoundAudio == null)
        {
            Debug.LogError("DoorPoundAudio is not assigned.");
        }

        if (heartbeatAudio == null)
        {
            Debug.LogError("HeartbeatAudio is not assigned.");
        }
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
        StartCoroutine(PlaySoundsAfterDelay(doorPoundDelay)); // Wait for the specified delay
    }

    private IEnumerator PlaySoundsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the door pound audio
        if (doorPoundAudio != null)
        {
            Debug.Log("Playing door pound audio.");
            doorPoundAudio.Play();
        }
        else
        {
            Debug.LogWarning("DoorPoundAudio is null. Cannot play sound.");
        }

        // Play the heartbeat audio
        if (heartbeatAudio != null)
        {
            heartbeatAudio.Play();
            Debug.Log("Playing heartbeat audio.");
        }
        else
        {
            Debug.LogWarning("HeartbeatAudio is null. Cannot play sound.");
        }
    }
}
