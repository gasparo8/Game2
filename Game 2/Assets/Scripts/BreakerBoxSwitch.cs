using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BreakerBoxSwitch : MonoBehaviour
{
    public GameObject walker;  // Reference the existing walker in the scene
    public AudioClip switchSound;  // Sound for the switch
    public AudioClip walkerSound;  // Sound for when the walker is enabled
    private AudioSource audioSource;
    private bool isSwitched = false;

    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    public PowerOutageScript powerOutageScript;
    public FlashlightController flashlightController;

    // Add a reference to the PostFlashlightTensionAudio script
    public PostFlashlightTensionAudio postFlashlightTensionAudio;

    public GameObject endGameTrigger;

    // Checkbox in the Inspector to set isSwitched
    [Header("Testing Options")]
    public bool enableSwitchTest; // This will act as the checkbox in the Inspector


    public Dialogue enemyDialogue;
    private DialogueManager dialogueManager;

    // Add a delay variable for the enemy dialogue
    [Header("Dialogue Settings")]
    public float dialogueDelay = 3f;  // Variable to set the delay time

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Make sure the walker is disabled at the start
        if (walker != null)
        {
            walker.SetActive(false);
        }

        if (endGameTrigger != null)
        {
            endGameTrigger.SetActive(false);
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength, layerMaskInteract))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    OnSwitchClicked();
                }
            }
        }
    }

    // This function is called whenever the Inspector value is changed
    private void OnValidate()
    {
        if (enableSwitchTest)
        {
            OnSwitchClicked(); // Call the switch clicked logic
            enableSwitchTest = false; // Reset the checkbox
        }
    }

    public void OnSwitchClicked()
{
    if (!isSwitched)
    {
        isSwitched = true;
        powerOutageScript.ResetPower();
        flashlightController.flashlight.enabled = false;

        // Stop the tension audio
        if (postFlashlightTensionAudio != null)
        {
            postFlashlightTensionAudio.StopTensionAudio();
        }

        // Play the switch sound
        if (switchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(switchSound);
        }

        // Enable the existing walker and loop the walker sound
        if (walker != null)
        {
            walker.SetActive(true);

            // Loop the walker sound
            if (walkerSound != null)
            {
                audioSource.clip = walkerSound;
                audioSource.loop = true;  // Enable looping
                audioSource.Play();  // Play the sound
            }
        }

        if (endGameTrigger != null)
        {
            endGameTrigger.SetActive(true);
        }

        // Start a coroutine to delay the dialogue start by 3 seconds
        if (dialogueManager != null) // Null check to avoid errors
        {
            StartCoroutine(StartEnemyDialogueWithDelay(dialogueDelay));
        }
    }
}

private IEnumerator StartEnemyDialogueWithDelay(float delay)
{
    yield return new WaitForSeconds(delay);  // Wait for 3 seconds
    dialogueManager.StartDialogue(enemyDialogue);  // Start the dialogue after the delay
}

    // Method to fade out the walker's sound
    public void FadeOutWalkerSound(float fadeDuration)
    {
        StartCoroutine(FadeOutAudio(fadeDuration));
    }

    // Coroutine to handle the audio fading over time
    private IEnumerator FadeOutAudio(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;  // Wait until the next frame
        }

        audioSource.Stop();  // Stop the sound once faded out
        audioSource.volume = startVolume;  // Reset the volume for future use
    }
}
