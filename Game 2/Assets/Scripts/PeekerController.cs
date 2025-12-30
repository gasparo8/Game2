/*using System.Collections;
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
            StartCoroutine(DestroyAfterDelay(4.0f));
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
*/
using System.Collections;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Animator anim;
    public GameObject peekerObject;

    [Header("Scare Light")]
    public Light scareLight;
    public float scareDuration = 2.0f;
    public float fadeOutDuration = 0.75f;

    private bool animationPlayed = false;
    private float originalIntensity;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        if (scareLight != null)
        {
            originalIntensity = scareLight.intensity;
            scareLight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !animationPlayed)
        {
            animationPlayed = true;
            anim.SetTrigger("PlayPeek");

            if (scareLight != null)
                StartCoroutine(ScareSequence());
        }
    }

    private IEnumerator ScareSequence()
    {
        // Turn light ON
        scareLight.intensity = originalIntensity;
        scareLight.enabled = true;

        // Hold light
        yield return new WaitForSeconds(scareDuration);

        // Fade OUT
        float timer = 0f;
        while (timer < fadeOutDuration)
        {
            scareLight.intensity = Mathf.Lerp(
                originalIntensity,
                0f,
                timer / fadeOutDuration
            );

            timer += Time.deltaTime;
            yield return null;
        }

        // Disable light cleanly
        scareLight.enabled = false;
        scareLight.intensity = originalIntensity;

        // Small pause for psychological aftertaste (optional but good)
        yield return new WaitForSeconds(0.25f);

        // Cleanup
        if (peekerObject != null)
            Destroy(peekerObject);

        Destroy(gameObject);
    }
}
