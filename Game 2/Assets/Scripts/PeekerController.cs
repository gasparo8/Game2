using System.Collections;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public GameObject peekerObject; // Reference to the FrontDoorPeeker GameObject
    private bool animationPlayed = false; // To ensure the animation plays only once

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        // Ensure the peekerObject is assigned
        if (peekerObject == null)
        {
            Debug.LogError("Peeker object not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player (use tags or layers)
        if (other.CompareTag("Player") && !animationPlayed)
        {
            // Set the trigger to play the animation
            anim.SetTrigger("PlayPeek");
            animationPlayed = true; // Ensure it only plays once

            // Schedule the peeker object for destruction 4 seconds after the animation is triggered
            StartCoroutine(DestroyAfterDelay(4f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the peeker GameObject
        if (peekerObject != null)
        {
            Destroy(peekerObject);
        }

        // Destroy the trigger GameObject
        Destroy(gameObject);
    }
}
