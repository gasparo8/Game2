using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutageScript : MonoBehaviour
{
    [System.Serializable]
    public class RendererMaterials
    {
        public Renderer objectRenderer;  // The renderer of the light object
        public Material onMaterial;      // Material for when the light is on
        public Material offMaterial;     // Material for when the light is off
    }

    [System.Serializable]
    public class LightObject
    {
        public Light lightSource;  // The light component (can be null if no real light)
        public List<RendererMaterials> renderers = new List<RendererMaterials>();  // List of renderers with materials to swap
    }

    public List<LightObject> lights = new List<LightObject>();  // List of lights and their corresponding renderers/materials

    [SerializeField]
    private bool triggerPowerOutage = false;  // Checkbox to test power outage
    [SerializeField]
    private bool restorePower = false;  // Checkbox to test restoring power

    private bool powerOut = false;  // Track the current power state

    // Reference to the ReadingSecondCutsceneManager
    public ReadingSecondCutsceneManager readingCutsceneManager;

    // Reference to an AudioClip for the power outage sound
    public AudioClip powerOutageSound;

    // AudioSource component to play the sound
    private AudioSource audioSource;

    private DialogueManager dialogueManager;
    public Dialogue postPowerOutageDialogue;

    void Start()
    {
        // Ensure dialogueManager is assigned properly
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Awake()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // Check the checkbox during testing for triggering power outage
        if (triggerPowerOutage && !powerOut)
        {
            TriggerOutage();
            triggerPowerOutage = false;  // Reset the checkbox
        }

        // Check the checkbox during testing for restoring power
        if (restorePower && powerOut)
        {
            ResetPower();
            restorePower = false;  // Reset the checkbox
        }
    }

    // Coroutine to delay the power outage by 10 seconds
    public IEnumerator PowerOutageCoroutine()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        TriggerOutage(); // Trigger power outage
        Debug.Log("Power outage triggered after cutscene.");
    }

    // Public method to start the coroutine
    public void StartPowerOutageCoroutine()
    {
        StartCoroutine(PowerOutageCoroutine());
        Debug.Log("Power outage coroutine started. Waiting 10 seconds.");
    }

    // Method to trigger the power outage
    public void TriggerOutage()
    {
        powerOut = true;
        LightSwitch.isPowerOut = true;  // Update the global flag

        // Play the power outage sound if it's set
        if (powerOutageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(powerOutageSound);
        }

        PostPowerOutageDialogue();


        foreach (LightObject lightObj in lights)
        {
            // Turn off the light
            if (lightObj.lightSource != null)
            {
                lightObj.lightSource.enabled = false;
            }

            // Swap materials for each renderer
            foreach (RendererMaterials rendererMaterial in lightObj.renderers)
            {
                if (rendererMaterial.objectRenderer != null && rendererMaterial.offMaterial != null)
                {
                    rendererMaterial.objectRenderer.material = rendererMaterial.offMaterial;
                }
            }
        }

        Debug.Log("Power outage triggered.");
    }

    // Method to reset power (restores lights and materials to 'on' state)
    public void ResetPower()
    {
        powerOut = false;
        LightSwitch.isPowerOut = false;  // Reset the global flag

        foreach (LightObject lightObj in lights)
        {
            // Turn on the light
            if (lightObj.lightSource != null)
            {
                lightObj.lightSource.enabled = true;
            }

            // Swap materials back to 'on' state
            foreach (RendererMaterials rendererMaterial in lightObj.renderers)
            {
                if (rendererMaterial.objectRenderer != null && rendererMaterial.onMaterial != null)
                {
                    rendererMaterial.objectRenderer.material = rendererMaterial.onMaterial;
                }
            }
        }
    }
    // Method to trigger the post-power outage dialogue
    public void PostPowerOutageDialogue()
    {
        if (dialogueManager != null && postPowerOutageDialogue != null)
        {
            dialogueManager.StartDialogue(postPowerOutageDialogue);
            Debug.Log("Post power outage dialogue triggered.");
        }
        else
        {
            Debug.LogWarning("DialogueManager or postPowerOutageDialogue not set.");
        }
    }
}
