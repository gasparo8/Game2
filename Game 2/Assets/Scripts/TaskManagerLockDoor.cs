using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<LockController> doors;
    [SerializeField] private TextMeshProUGUI lockedCounterText;
    [SerializeField] private BookDialogueTrigger bookDialogueTrigger; // Reference to the BookDialogueTrigger

    private int lockedDoorsCount = 0;

    private void Start()
    {
        lockedCounterText.gameObject.SetActive(false); // Hide the counter initially
        UpdateLockedCounter();
    }

    public void DoorLockedStateChanged(bool isLocked)
    {
        if (isLocked)
        {
            lockedDoorsCount++;
        }
        else
        {
            lockedDoorsCount--;
        }

        UpdateLockedCounter();
    }

    private void UpdateLockedCounter()
    {
        if (lockedDoorsCount >= doors.Count)
        {
            lockedCounterText.gameObject.SetActive(false); // Hide the counter if all doors are locked
            bookDialogueTrigger.GoGetBookDialogue(); // Trigger the book dialogue
            this.enabled = false; // Disable this script to prevent further updates
        }
        else
        {
            lockedCounterText.text = $"{lockedDoorsCount}/{doors.Count} Doors Locked";
        }
    }

    // Method to handle cutscene completion
    public void OnCutsceneComplete()
    {
        lockedCounterText.gameObject.SetActive(true); // Show the counter
    }
}