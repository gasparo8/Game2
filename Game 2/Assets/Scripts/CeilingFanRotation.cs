using UnityEngine;

public class CeilingFanRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of the ceiling fan rotation

    void Update()
    {
        // Rotate the fan around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}