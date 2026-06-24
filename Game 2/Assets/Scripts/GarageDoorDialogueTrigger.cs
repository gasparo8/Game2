using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    [Header("Options")]
    public bool disableColliderAfterTriggered = true;
    public float dialogueDelay = 2f; // <-- set this in Inspector

    private Collider triggerCollider;
    private bool hasTriggered = false;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(StartDialogueWithDelay());
        }
    }

    private IEnumerator StartDialogueWithDelay()
    {
        yield return new WaitForSeconds(dialogueDelay);

        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogue);
        }

        if (disableColliderAfterTriggered && triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }
}