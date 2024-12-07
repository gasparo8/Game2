using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyRoomPEEK : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public GameObject peekerObject; // Reference to the FrontDoorPeeker GameObject
    public List<Light> pointLights; // List of lights that will flicker
    private bool animationPlayed = false; // To ensure the animation plays only once
    public ShedNoConcern shedNoConcernScript; // Reference to the ShedNoConcern script
    public GameObject knifeObject; // Reference to the knife GameObject

    private DialogueManager dialogueManager;
    public Dialogue postFamilyRoomPeekDialogue;
    private int postFamilyRoomPeekDialogueDelay = 8;

    public float minFlickerTime = 0.1f; // Minimum flicker interval
    public float maxFlickerTime = 0.5f; // Maximum flicker interval

    private Coroutine flickerCoroutine;

    public bool hasAnimationPlayed = false; // Public variable to track animation state

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (peekerObject != null)
        {
            peekerObject.SetActive(false); // Ensure the friend object is disabled at the start
        }

        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }

        if (pointLights == null || pointLights.Count == 0)
        {
            Debug.LogWarning("No point lights assigned for flickering!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ShedNoConcern trigger has been activated before playing the animation
        if (other.CompareTag("Player") && !animationPlayed && shedNoConcernScript.hasTriggered)
        {
            // Delete the knife
            if (knifeObject != null)
            {
                Destroy(knifeObject); // Delete the knife
            }

            peekerObject.SetActive(true);
            // Set the trigger to play the animation
            anim.SetTrigger("PlayFamilyRoomPeek");
            animationPlayed = true; // Ensure it only plays once
            hasAnimationPlayed = true; // Update the public variable

            // Start the light flickering coroutine when the animation starts
            if (pointLights != null && pointLights.Count > 0)
            {
                flickerCoroutine = StartCoroutine(FlickerLights());
            }

            StartCoroutine(PostFamilyRoomPeekDialogue());
        }
    }

    // Coroutine to flicker all point lights on and off
    private IEnumerator FlickerLights()
    {
        while (true) // Keep flickering while the animation is playing
        {
            float flickerTime = Random.Range(minFlickerTime, maxFlickerTime);
            foreach (Light light in pointLights)
            {
                if (light != null) // Ensure the light is valid
                {
                    light.enabled = !light.enabled; // Toggle the light on/off
                }
            }
            yield return new WaitForSeconds(flickerTime);
        }
    }

    // Method to stop the flickering
    private void StopFlickering()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            foreach (Light light in pointLights)
            {
                if (light != null)
                {
                    light.enabled = true; // Ensure lights are on when flickering ends
                }
            }
        }
    }

    // Coroutine for delayed post-animation dialogue
    private IEnumerator PostFamilyRoomPeekDialogue()
    {
        // Wait for the specified delay before starting the dialogue
        yield return new WaitForSeconds(postFamilyRoomPeekDialogueDelay);

        if (animationPlayed)
        {
            dialogueManager.StartDialogue(postFamilyRoomPeekDialogue);
        }

        // Stop flickering after the dialogue delay
        StopFlickering();
    }
}
