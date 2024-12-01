using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathButtons : MonoBehaviour
{
    public GameObject deathRestartButton;  // Assign in Unity Inspector
    public GameObject deathMainMenuButton; // Assign in Unity Inspector
    public MouseLook mouseLook;           // Reference to MouseLook script to unlock the cursor
    public GameObject deathButtons;

    private bool buttonsEnabled = false; // Flag to prevent repeated coroutine execution

    void Start()
    {
        deathButtons.SetActive(false);

        // Ensure buttons are initially disabled
        if (deathRestartButton != null) deathRestartButton.SetActive(false);
        if (deathMainMenuButton != null) deathMainMenuButton.SetActive(false);
    }

    // Call this method when the player dies
    public void OnPlayerDeathforButtons()
    {
        if (!buttonsEnabled) // Ensure this runs only once
        {
            buttonsEnabled = true;
            deathButtons.SetActive(true);

            // Start coroutine to enable buttons after 4 seconds
            StartCoroutine(EnableButtonsAfterDelay(4f));
        }
    }

    private IEnumerator EnableButtonsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Enable buttons
        if (deathRestartButton != null) deathRestartButton.SetActive(true);
        if (deathMainMenuButton != null) deathMainMenuButton.SetActive(true);
    }
}
