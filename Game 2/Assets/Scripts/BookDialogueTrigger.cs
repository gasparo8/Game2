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
    public float postHeartbeatDelay = 5f;  // Delay after the heartbeat before triggering the dogs barking

    public GameObject slideDoorBlocker;
    public GameObject sideHouseBlocker1;
    public GameObject sideHouseBlocker2;

    void Start()
    {
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }

        if (slideDoorBlocker != null)
        {
            slideDoorBlocker.SetActive(false); // Ensure the sliderDoorBlocker cam is initially inactive
        }

        if (sideHouseBlocker1 != null)
        {
            sideHouseBlocker1.SetActive(false); // Ensure the sliderDoorBlocker cam is initially inactive
        }

        if (sideHouseBlocker2 != null)
        {
            sideHouseBlocker2.SetActive(false); // Ensure the sliderDoorBlocker cam is initially inactive
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
            slideDoorBlocker.SetActive(true);
            sideHouseBlocker1.SetActive(true);
            sideHouseBlocker2.SetActive(true);
        }

        // Play the heartbeat audio and wait for it to finish
        if (heartbeatAudio != null)
        {
            heartbeatAudio.Play();
            yield return new WaitForSeconds(heartbeatAudio.clip.length);
        }

        // Wait an additional 5 seconds after the heartbeat ends
        yield return new WaitForSeconds(postHeartbeatDelay);

        // Trigger the DogsBarking script
        if (dogsBarkingScript != null)
        {
            dogsBarkingScript.StartBarking();  // Ensure DogsBarking has a method to start the barking
        }
    }
}
