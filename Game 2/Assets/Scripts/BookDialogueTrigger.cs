/*using System.Collections;
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
*/

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

        // Ensure necessary references are assigned
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
        }
        if (doorPoundAudio == null)
        {
            Debug.LogError("DoorPoundAudio is not assigned.");
        }
        if (heartbeatAudio == null)
        {
            Debug.LogError("HeartbeatAudio is not assigned.");
        }
        if (dogsBarkingScript == null)
        {
            Debug.LogError("DogsBarkingScript is not assigned.");
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
        StartCoroutine(PlaySoundsAfterDelay(doorPoundDelay));  // Wait for the specified delay
    }

    private IEnumerator PlaySoundsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the door pound audio
        if (doorPoundAudio != null)
        {
            doorPoundAudio.Play();
            Debug.Log("Playing door pound audio.");
        }

        // Play the heartbeat audio and wait for it to finish
        if (heartbeatAudio != null)
        {
            heartbeatAudio.Play();
            Debug.Log("Playing heartbeat audio.");
            yield return new WaitForSeconds(heartbeatAudio.clip.length);
        }

        // Wait an additional 15 seconds after the heartbeat ends
        yield return new WaitForSeconds(postHeartbeatDelay);

        // Trigger the DogsBarking script
        if (dogsBarkingScript != null)
        {
            dogsBarkingScript.StartBarking();  // Ensure DogsBarking has a method to start the barking
            Debug.Log("Triggered dogs barking.");
        }
    }
}
