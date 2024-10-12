using UnityEngine;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlight; // Reference to the Light component
    [SerializeField] private GameObject flashlightBodyMesh; // Reference to the flashlight body mesh
    [SerializeField] private GameObject flashlightLensMesh; // Reference to the flashlight lens mesh
    [SerializeField] private float followDelay = 0.1f; // Delay for the flashlight to follow the camera

    private void Start()
    {
        // Ensure the flashlight light is off and both meshes are enabled initially
        if (flashlight != null)
        {
            flashlight.enabled = false; // Light off at start
        }

        if (flashlightBodyMesh != null)
        {
            flashlightBodyMesh.SetActive(true); // Flashlight body mesh visible at start
        }

        if (flashlightLensMesh != null)
        {
            flashlightLensMesh.SetActive(true); // Flashlight lens mesh visible at start
        }
    }

    public void EnableFlashlight()
    {
        if (flashlight != null && flashlightBodyMesh != null && flashlightLensMesh != null)
        {
            StartCoroutine(EnableFlashlightWithDelay()); // Start the coroutine to enable the flashlight
        }
    }

    private IEnumerator EnableFlashlightWithDelay()
    {
        // Optional delay before enabling the flashlight
        yield return new WaitForSeconds(0.2f);

        flashlight.enabled = true; // Enable the light component
        flashlightBodyMesh.SetActive(false); // Disable the body mesh (flashlight is in use)
        flashlightLensMesh.SetActive(false); // Disable the lens mesh as well
        Debug.Log("Flashlight enabled!");
    }
}
