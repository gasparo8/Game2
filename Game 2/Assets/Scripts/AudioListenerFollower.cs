using UnityEngine;

public class AudioListenerFollower : MonoBehaviour
{
    public Camera[] cameras; // Array of cameras used in the cutscene

    void Update()
    {
        // Find the currently active camera
        Camera activeCamera = null;
        foreach (var cam in cameras)
        {
            if (cam.isActiveAndEnabled)
            {
                activeCamera = cam;
                break;
            }
        }

        // Make the AudioListenerObject follow the active camera
        if (activeCamera != null)
        {
            transform.position = activeCamera.transform.position;
            transform.rotation = activeCamera.transform.rotation;
        }
    }
}
