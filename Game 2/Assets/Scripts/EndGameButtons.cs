using System.Collections;
using UnityEngine;

public class EndGameButtons : MonoBehaviour
{
    public GameObject restartButton;  // Assign in Unity Inspector
    public GameObject mainMenuButton; // Assign in Unity Inspector
    public MouseLook mouseLook;       // Reference to MouseLook script to unlock the cursor

    void Start()
    {
        // Ensure buttons are initially disabled
        if (restartButton != null) restartButton.SetActive(false);
        if (mainMenuButton != null) mainMenuButton.SetActive(false);
    }

    // Call this method when the player dies
    public void OnPlayerDeathforButtons()
    {
        // Start coroutine to enable buttons after 3 seconds
        StartCoroutine(EnableButtonsAfterDelay(3f));
    }

    private IEnumerator EnableButtonsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Enable buttons
        if (restartButton != null) restartButton.SetActive(true);
        if (mainMenuButton != null) mainMenuButton.SetActive(true);
    }
}
