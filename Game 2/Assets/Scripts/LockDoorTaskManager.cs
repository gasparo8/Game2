using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<LockController> doors;
    [SerializeField] private TextMeshProUGUI lockedCounterText;

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
        lockedCounterText.text = $"{lockedDoorsCount}/{doors.Count} Locked Doors";
    }

    // Method to handle cutscene completion
    public void OnCutsceneComplete()
    {
        lockedCounterText.gameObject.SetActive(true); // Show the counter
    }
}
