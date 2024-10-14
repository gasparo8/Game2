using UnityEngine;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] public Light flashlight; // Reference to the Light component
    [SerializeField] private GameObject flashlightBodyMesh; // Reference to the flashlight body mesh
    [SerializeField] private GameObject flashlightLensMesh; // Reference to the flashlight lens mesh
    [SerializeField] private AudioClip flashlightToggleSound; // Sound played when flashlight is toggled
    private AudioSource audioSource; // Audio source for playing sounds

    private DialogueManager dialogueManager;
    public Dialogue pickedUpFlashlightDialogue;

    [SerializeField] private GameObject postFlashlightTensionTrigger; // Reference to the trigger object
    [SerializeField] private GameObject postFlashlightMaskTrigger; // Reference to the trigger object

    private void Start()
    {
        // Ensure the flashlight light is off and both meshes are enabled initially
        if (flashlight != null)
        {
            flashlight.enabled = false; // Light off at start
        }

        if (flashlightBodyMesh != null)
        {
            flashlightBodyMesh.SetActive(true); // Flashlight body mesh visible at start
        }

        if (flashlightLensMesh != null)
        {
            flashlightLensMesh.SetActive(true); // Flashlight lens mesh visible at start
        }

        postFlashlightTensionTrigger.SetActive(false); // Disable trigger initially
        postFlashlightMaskTrigger.SetActive(false); // Disable trigger initially

        // Ensure the audio source is ready
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }

        // Ensure dialogueManager is assigned properly
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void EnableFlashlight()
    {
        if (flashlight != null && flashlightBodyMesh != null && flashlightLensMesh != null)
        {
            PlayToggleSound(); // Play the flashlight toggle sound
            PickedUpFlashlightDialogue();
            StartCoroutine(EnableFlashlightWithDelay()); // Start the coroutine to enable the flashlight
        }
    }

    private IEnumerator EnableFlashlightWithDelay()
    {
        // Optional delay before enabling the flashlight
        yield return new WaitForSeconds(0.2f);

        flashlight.enabled = true; // Enable the light component
        flashlightBodyMesh.SetActive(false); // Disable the body mesh (flashlight is in use)
        flashlightLensMesh.SetActive(false); // Disable the lens mesh as well
        Debug.Log("Flashlight enabled!");


        // Enable the PostFlashlightTension trigger object
        if (postFlashlightTensionTrigger != null)
        {
            postFlashlightTensionTrigger.SetActive(true);
            Debug.Log("PostFlashlightTension trigger enabled!");
            postFlashlightMaskTrigger.SetActive(true);
        }


    }

    // Play the flashlight toggle sound
    private void PlayToggleSound()
    {
        if (flashlightToggleSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(flashlightToggleSound); // Play the sound once when flashlight is toggled
        }
    }

    public void PickedUpFlashlightDialogue()
    {
        if (dialogueManager != null && pickedUpFlashlightDialogue != null)
        {
            dialogueManager.StartDialogue(pickedUpFlashlightDialogue);
            Debug.Log("Picked Up Flashlight dialogue triggered.");
        }
        else
        {
            Debug.LogWarning("DialogueManager or Flashlight not set.");
        }
    }
}
