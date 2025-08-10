using UnityEngine;
using System.Collections;

public class TVStaticController : MonoBehaviour
{
    [SerializeField] private GameObject tvStaticObject; // The Quad with video
    [SerializeField] private float displayTime = 3.5f; // How long to show static
    [SerializeField] private DialogueTrigger dialogueTrigger; // Reference to DialogueTrigger

    private bool hasTriggered = false;

    private void Start()
    {
        if (tvStaticObject != null)
            tvStaticObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(PlayStaticThenDialogue());
        }
    }

    private IEnumerator PlayStaticThenDialogue()
    {
        if (tvStaticObject != null)
            tvStaticObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        if (tvStaticObject != null)
            Destroy(tvStaticObject);

        // Trigger your "Okay.." dialogue
        if (dialogueTrigger != null)
            dialogueTrigger.TriggerDialogue();

        // Optionally, destroy the trigger
        Destroy(gameObject);
    }
}
