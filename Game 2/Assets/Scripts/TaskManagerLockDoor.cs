using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<LockController> doors;
    [SerializeField] private TextMeshProUGUI lockedCounterText;
    [SerializeField] private BookDialogueTrigger bookDialogueTrigger;

    [SerializeField] private PhoneController phoneController; // Assign in Inspector

    private int lockedDoorsCount = 0;
    private bool allDoorsLocked = false;
    private bool firstDoorTriggeredPhone = false; // Prevent repeat trigger

    public GameObject frontDoorPeeker;
    public static bool ShowLockHighlights = false;
    public GameObject tvStaticObject;

    private void Start()
    {
        lockedCounterText.gameObject.SetActive(false);
        ShowLockHighlights = false;
        UpdateLockedCounter();
        frontDoorPeeker.SetActive(false);
    }

    public void DoorLockedStateChanged(bool isLocked)
    {
        if (!allDoorsLocked)
        {
            if (isLocked)
            {
                lockedDoorsCount++;

                // First locked door only
                if (lockedDoorsCount == 1 && !firstDoorTriggeredPhone)
                {
                    firstDoorTriggeredPhone = true;
                    StartCoroutine(PlayPhoneAfterDelay());
                }
            }
            else
            {
                lockedDoorsCount--;
            }

            UpdateLockedCounter();
        }
    }

    private IEnumerator PlayPhoneAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (phoneController != null)
        {
            phoneController.PlayPhoneAnimation();
        }
    }

    private void UpdateLockedCounter()
    {
        if (lockedDoorsCount >= doors.Count)
        {
            lockedCounterText.gameObject.SetActive(false);
            bookDialogueTrigger.GoGetBookDialogue();
            allDoorsLocked = true;
            ShowLockHighlights = false;

            foreach (var door in doors)
            {
                LockHighlight lockHighlight = door.GetComponent<LockHighlight>();

                if (lockHighlight != null)
                {
                    lockHighlight.HighlightLock(false);
                    lockHighlight.enabled = false;
                }
            }

            this.enabled = false;
        }
        else
        {
            lockedCounterText.text = $"{lockedDoorsCount}/{doors.Count} Doors Locked";
        }
    }

    public void OnCutsceneComplete()
    {
        if (!allDoorsLocked)
        {
            lockedCounterText.gameObject.SetActive(true);
            frontDoorPeeker.gameObject.SetActive(true);
            ShowLockHighlights = true;

            if (tvStaticObject != null)
                tvStaticObject.SetActive(true);
        }
    }
}