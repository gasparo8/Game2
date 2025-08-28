using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintManager : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger; // Assign in Inspector
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
            }
        }
    }
}
