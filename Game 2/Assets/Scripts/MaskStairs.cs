using UnityEngine;
using System.Collections;

public class MaskStairsDescend : MonoBehaviour
{
    public Animator objectAnimator;  // Reference to the Animator component
    public string MaskStairs;     // Name of the animation to play
    public GameObject objectToDisable;  // The object to disable after the animation
    private bool hasPlayed = false;  // To ensure the animation only plays once

    private void Start()
    {
        // Disable the object at the start
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }

    // Called when something enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;  // Prevent the animation from playing multiple times

            // Enable the object
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(true);
            }

            // Play the animation
            if (objectAnimator != null)
            {
                objectAnimator.Play(MaskStairs);

                // Start checking for when the animation finishes
                StartCoroutine(DisableObjectAfterAnimation());
            }
        }
    }

    private IEnumerator DisableObjectAfterAnimation()
    {
        // Get the AnimatorStateInfo to monitor animation progress
        AnimatorStateInfo animStateInfo = objectAnimator.GetCurrentAnimatorStateInfo(0);

        // Wait until the animation has finished
        while (animStateInfo.normalizedTime < 1.0f)
        {
            yield return null; // Wait for the next frame
            animStateInfo = objectAnimator.GetCurrentAnimatorStateInfo(0); // Update the animation state info
        }

        // Disable the object after the animation completes
        objectToDisable.SetActive(false);
    }
}