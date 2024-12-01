using System.Collections;
using UnityEngine;

public class EndGameButtons : MonoBehaviour
{
    public GameObject restartButton;  // Assign in Unity Inspector
    public GameObject mainMenuButton; // Assign in Unity Inspector
    public MouseLook mouseLook;       // Reference to MouseLook script to unlock the cursor
    public GameObject endGameButtons;

    private bool buttonsEnabled = false; // Flag to prevent repeated coroutine execution

    void Start()
    {
        endGameButtons.SetActive(false);

        // Ensure buttons are initially disabled
        if (restartButton != null) restartButton.SetActive(false);
        if (mainMenuButton != null) mainMenuButton.SetActive(false);
    }

    public void EnableButtonsOnGameComplete()
    {
        if (!buttonsEnabled) // Ensure this runs only once
        {
            buttonsEnabled = true;
            endGameButtons.SetActive(true);

            // Start coroutine to enable buttons after 20 seconds
            StartCoroutine(EnableButtonsAfterCameraScrollEndGame(20f));
        }
    }

    private IEnumerator EnableButtonsAfterCameraScrollEndGame(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Enable buttons
        if (restartButton != null) restartButton.SetActive(true);
        if (mainMenuButton != null) mainMenuButton.SetActive(true);
    }
}
