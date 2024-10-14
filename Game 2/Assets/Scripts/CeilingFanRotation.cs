/*using UnityEngine;

public class CeilingFanRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of the ceiling fan rotation
    public PowerOutageScript powerOutageScript;  // Reference to the PowerOutageScript

    void Update()
    {
        // Check if the power is out before rotating the fan
        if (powerOutageScript != null && !powerOutageScript.IsPowerOut())
        {
            // Rotate the fan around the Y-axis if the power is on
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
*/

using UnityEngine;

public class CeilingFanRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Initial speed of the ceiling fan rotation
    public float decelerationRate = 10f;  // How quickly the fan decelerates
    public PowerOutageScript powerOutageScript;  // Reference to the PowerOutageScript
    private bool isDecelerating = false;  // Whether the fan is decelerating

    void Update()
    {
        // Check if the power is out
        if (powerOutageScript != null && powerOutageScript.IsPowerOut())
        {
            if (!isDecelerating)
            {
                isDecelerating = true;  // Start decelerating the fan
            }

            // If decelerating, slowly reduce the rotation speed to 0
            if (rotationSpeed > 0f)
            {
                rotationSpeed -= decelerationRate * Time.deltaTime;
                rotationSpeed = Mathf.Max(rotationSpeed, 0f);  // Clamp to 0 to avoid negative values
            }
        }
        else
        {
            // If power is restored, reset the rotation speed and stop deceleration
            rotationSpeed = 100f;
            isDecelerating = false;
        }

        // Rotate the fan if the speed is greater than 0
        if (rotationSpeed > 0f)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}