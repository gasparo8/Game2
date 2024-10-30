using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    // Reference to the object to toggle (e.g., monitor screen)
    public GameObject objectToToggle;

    // A flag to check the current state of the object
    private bool isObjectOn = false;

    // Method to turn the object on
    public void TurnObjectOn()
    {
        if (!isObjectOn)
        {
            objectToToggle.SetActive(true);
            isObjectOn = true;
            Debug.Log(objectToToggle.name + " turned ON.");
        }
    }

    // Method to turn the object off
    public void TurnObjectOff()
    {
        if (isObjectOn)
        {
            objectToToggle.SetActive(false);
            isObjectOn = false;
            Debug.Log(objectToToggle.name + " turned OFF.");
        }
    }
}
