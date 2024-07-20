using System.Collections;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public string animationName = "FrontDoorPeek"; // Name of the animation to play

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player (you can use tags or layers)
        if (other.CompareTag("Player"))
        {
            // Play the animation
            Debug.Log("Trigger entered by Player. Playing animation...");
            anim.Play(animationName);
            Debug.Log("Peeker anim played");

            // Schedule the object for destruction after the animation has finished
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait until the animation is playing
        yield return null; // Wait for one frame to ensure the animation state has changed

        // Get the length of the current animation state
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        Debug.Log("Animation Length: " + animationLength);

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationLength);

        // Destroy the GameObject
        Debug.Log("Destroying GameObject after animation.");
        Destroy(gameObject);
    }
}