/*using System.Collections;
using UnityEngine;

public class DogsBarking : MonoBehaviour
{
    public AudioSource dogsBarkingAudio; // Reference to the dogs barking audio
    public GameObject dogsBarkingCutscene; // Reference to the cutscene GameObject
    public Camera dogsBarkingCamera; // Reference to the cutscene camera
    public GameObject player; // Reference to the player
    public GameObject playerCamera; // Reference to the player's camera
    public LightFlicker lightsFlickerScript; // Reference to the LightFlicker script

    public float barkingDuration = 10f; // How long the dogs will bark
    public float cutsceneDelay = 4f; // Time after barking starts before the cutscene starts

    private Coroutine flickerCoroutine; // Coroutine reference to stop the flickering later

    private void Start()
    {
        // Ensure the cutscene camera is also initially disabled
        if (dogsBarkingCamera != null)
        {
            dogsBarkingCamera.enabled = false;
        }
    }

    public void StartBarking()
    {
        StartCoroutine(PlayDogsBarking());
    }

    private IEnumerator PlayDogsBarking()
    {
        // Play dogs barking sound
        if (dogsBarkingAudio != null)
        {
            dogsBarkingAudio.Play();
        }

        // Start flickering lights
        if (lightsFlickerScript != null)
        {
            flickerCoroutine = StartCoroutine(lightsFlickerScript.FlickerLight());
        }

        // Wait for the cutscene delay time
        yield return new WaitForSeconds(cutsceneDelay);

        // Disable player and player's camera for cutscene
        if (player != null)
        {
            player.SetActive(false);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(false);
        }

        // Activate cutscene and enable cutscene camera
        if (dogsBarkingCutscene != null && dogsBarkingCamera != null)
        {
            dogsBarkingCutscene.SetActive(true);
            dogsBarkingCamera.enabled = true;
        }

        // Wait for the remaining barking duration before ending the cutscene
        yield return new WaitForSeconds(barkingDuration - cutsceneDelay);

        // Re-enable the player and player's camera after the cutscene
        if (player != null)
        {
            player.SetActive(true);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(true);
        }

        // Disable cutscene camera and its AudioListener after the cutscene ends
        if (dogsBarkingCamera != null)
        {
            dogsBarkingCamera.enabled = false;
        }
    }
}
*/
using System.Collections;
using UnityEngine;

public class DogsBarking : MonoBehaviour
{
    public AudioSource dogsBarkingAudio; // Reference to the dogs barking audio
    public Camera dogsBarkingCamera; // Reference to the cutscene camera
    public GameObject player; // Reference to the player
    public GameObject playerCamera; // Reference to the player's camera
    public LightFlicker lightsFlickerScript; // Reference to the LightFlicker script

    public float barkingDuration = 10f; // How long the dogs will bark
    public float cutsceneDelay = 4f; // Time after barking starts before the camera swap

    private Coroutine flickerCoroutine; // Coroutine reference to stop the flickering later

    private void Start()
    {
        // Ensure the cutscene camera is also initially disabled
        if (dogsBarkingCamera != null)
        {
            dogsBarkingCamera.enabled = false;
        }
    }

    public void StartBarking()
    {
        StartCoroutine(PlayDogsBarking());
    }

    private IEnumerator PlayDogsBarking()
    {
        // Play dogs barking sound
        if (dogsBarkingAudio != null)
        {
            dogsBarkingAudio.Play();
        }

        // Start flickering lights
        if (lightsFlickerScript != null)
        {
            flickerCoroutine = StartCoroutine(lightsFlickerScript.FlickerLight());
        }

        // Wait for the cutscene delay time
        yield return new WaitForSeconds(cutsceneDelay);

        // Disable player and player's camera for cutscene
        if (player != null)
        {
            player.SetActive(false);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(false);
        }

        // Enable cutscene camera
        if (dogsBarkingCamera != null)
        {
            dogsBarkingCamera.enabled = true;
        }

        // Wait for the remaining barking duration before ending the cutscene
        yield return new WaitForSeconds(barkingDuration - cutsceneDelay);

        // Re-enable the player and player's camera after the cutscene
        if (player != null)
        {
            player.SetActive(true);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(true);
        }

        // Disable cutscene camera after the cutscene ends
        if (dogsBarkingCamera != null)
        {
            dogsBarkingCamera.enabled = false;
        }
    }
}
