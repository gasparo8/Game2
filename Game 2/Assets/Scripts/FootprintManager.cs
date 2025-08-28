using System.Collections;
using UnityEngine;

public class FootprintManager : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger; // Assign in Inspector
    public GameObject couchWalkPoint;       // Assign the couch walk point in Inspector

    private int totalFootprints;
    private int cleanedFootprints = 0;

    void Start()
    {
        // Count how many footprints are in the scene at the start
        totalFootprints = FindObjectsOfType<Footprint>().Length;
        Debug.Log("Total footprints: " + totalFootprints);
    }

    public void FootprintCleaned()
    {
        cleanedFootprints++;
        Debug.Log("Footprints cleaned: " + cleanedFootprints + "/" + totalFootprints);

        if (cleanedFootprints >= totalFootprints)
        {
            Debug.Log("All footprints cleaned!");
            if (dialogueTrigger != null)
            {
                dialogueTrigger.TriggerDialogue();
                StartCoroutine(ActivateCouchWalkPointAfterDelay(3f)); // wait 3 seconds
            }
        }
    }

    private IEnumerator ActivateCouchWalkPointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (couchWalkPoint != null)
        {
            couchWalkPoint.SetActive(true);
            Debug.Log("Couch walk point activated after dialogue delay.");
        }
    }
}
