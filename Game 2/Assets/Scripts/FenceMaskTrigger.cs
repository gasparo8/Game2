/*using System.Collections;
using UnityEngine;

public class FenceMaskTrigger : MonoBehaviour
{
    public GameObject maskObject;     // Assign your mask in Inspector
    public float visibleTime = 8f;    // How long mask stays visible

    private bool hasTriggered = false;

    void Start()
    {
        if (maskObject != null)
        {
            maskObject.SetActive(false); // Hidden at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ShowMaskTemporarily());
        }
    }

    private IEnumerator ShowMaskTemporarily()
    {
        if (maskObject != null)
        {
            maskObject.SetActive(true);
        }

        yield return new WaitForSeconds(visibleTime);

        if (maskObject != null)
        {
            maskObject.SetActive(false);
        }
    }
}
*/

using UnityEngine;

public class FenceMaskTrigger : MonoBehaviour
{
    public Animator maskAnimator;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            maskAnimator.Play("MaskFenceMovement");
        }
    }
}