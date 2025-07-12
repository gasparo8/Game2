using System.Collections;
using UnityEngine;

public class BatTriggerController : MonoBehaviour
{
    public Animator anim;
    public GameObject batObject;
    private bool animationPlayed = false;

    private void Start()
    {
        if (batObject == null)
        {
            Debug.LogError("Bat object not assigned!");
            return;
        }

        // Ensure the bat starts inactive
        batObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !animationPlayed)
        {
            animationPlayed = true;

            // Activate the bat GameObject
            batObject.SetActive(true);

            // Get the Animator component (since it may not exist while inactive)
            if (anim == null)
                anim = batObject.GetComponent<Animator>();

            if (anim != null)
            {
                anim.SetTrigger("PlayBatAnim");
            }
            else
            {
                Debug.LogWarning("Animator not found on batObject after activation.");
            }

            StartCoroutine(DestroyAfterDelay(4f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (batObject != null) Destroy(batObject);
        Destroy(gameObject);
    }
}

