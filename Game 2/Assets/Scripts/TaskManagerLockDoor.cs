using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<LockController> doors;
    [SerializeField] private TextMeshProUGUI lockedCounterText;
    [SerializeField] private BookDialogueTrigger bookDialogueTrigger; // Reference to the BookDialogueTrigger

    private int lockedDoorsCount = 0;
    private bool allDoorsLocked = false; // Flag to track if all doors have been locked

    public GameObject frontDoorPeeker;

    public static bool ShowLockHighlights = false;

    public GameObject tvStaticObject; // Drag TV Static GameObject here in Inspector

    private void Start()
    {
        lockedCounterText.gameObject.SetActive(false); // Hide the counter initially
        ShowLockHighlights = false;
        UpdateLockedCounter();
        frontDoorPeeker.SetActive(false);
    }

    public void DoorLockedStateChanged(bool isLocked)
    {
        // Only allow updates if not all doors have been locked before
        if (!allDoorsLocked)
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
    }

    private void UpdateLockedCounter()
    {
        if (lockedDoorsCount >= doors.Count)
        {
            lockedCounterText.gameObject.SetActive(false); // Hide the counter if all doors are locked
            bookDialogueTrigger.GoGetBookDialogue(); // Trigger the book dialogue
            allDoorsLocked = true; // Mark all doors as locked, preventing further changes
            ShowLockHighlights = false;

            // Disable LockHighlight script and reset the highlight color
            foreach (var door in doors)
            {
                LockHighlight lockHighlight = door.GetComponent<LockHighlight>();
                if (lockHighlight != null)
                {
                    lockHighlight.HighlightLock(false); // Reset the color
                    lockHighlight.enabled = false; // Disable the LockHighlight script
                }
            }

            this.enabled = false; // Disable TaskManager to prevent further updates
        }
        else
        {
            lockedCounterText.text = $"{lockedDoorsCount}/{doors.Count} Doors Locked";
        }
    }

    // Method to handle cutscene completion
    public void OnCutsceneComplete()
    {
        if (!allDoorsLocked)
        {
            lockedCounterText.gameObject.SetActive(true); // Show the counter
            frontDoorPeeker.gameObject.SetActive(true);
            ShowLockHighlights = true;

            // Enable TV Static
            if (tvStaticObject != null)
                tvStaticObject.SetActive(true);
        }
    }
}
