using System.Collections;
using UnityEngine;
using UnityEngine.Playables; // Required for Timeline

public class EndOfGameManager : MonoBehaviour
{
    // Reference to the cop car
    public GameObject copCar;

    // Reference to the PlayableDirector for Timeline cutscene
    public PlayableDirector endGameCutsceneDirector;

    // Reference to the main camera
    public Camera mainCamera;

    // Reference to the end game pan camera
    public Camera endGamePanCamera;

    // Time in seconds to wait before switching camera and enabling cop car (middle of fade animation)
    public float timeToSwitchCameraAndEnableCopCar = 1.0f; // Adjust this based on your animation timing

    public float timeCopCarEnable = 1.0f;

    private void Start()
    {
        // Ensure the end game pan camera is disabled at the start
        if (endGamePanCamera != null)
        {
            endGamePanCamera.enabled = false;
        }
    }

    // Function to trigger the end game cutscene
    public void TriggerEndGameCutscene()
    {
        // Step 1: Play the end game cutscene (fade to black, etc.)
        if (endGameCutsceneDirector != null)
        {
            endGameCutsceneDirector.Play();  // Play the Timeline cutscene

            // Step 2: Start coroutine to switch the camera and enable the cop car after the fade-to-black
            StartCoroutine(SwitchCameraAndEnableCopCar());
        }
    }

    // Coroutine for handling camera switch and enabling cop car at the correct time
    private IEnumerator SwitchCameraAndEnableCopCar()
    {
        // Step 3: Wait until the middle of the fade animation (adjust based on the cutscene timing)
        yield return new WaitForSeconds(timeToSwitchCameraAndEnableCopCar);

        // Step 4: Disable the main camera and enable the end game pan camera
        if (mainCamera != null && endGamePanCamera != null)
        {
            mainCamera.enabled = false;
            endGamePanCamera.enabled = true;
        }

        // Step 5: Wait for an additionaltime before enabling the cop car
        yield return new WaitForSeconds(timeCopCarEnable);

        // Step 6: Enable the cop car
        copCar.SetActive(true);
    }
}
