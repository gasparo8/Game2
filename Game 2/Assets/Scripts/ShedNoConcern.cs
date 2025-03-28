using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedNoConcern : MonoBehaviour
{
    public FriendJumpScare friendJumpScareScript; // Reference to the FriendJumpScare script
    public GameObject triggerObject; // The object you want to enable (the shed trigger)
    public bool hasTriggered = false; // Boolean to track if the player has triggered the shed

    public DoorController frontDoorController; // Reference to the Front Door Controller

    private void Start()
    {
        // Ensure the trigger object is disabled at the start
        if (triggerObject != null)
        {
            triggerObject.SetActive(false);
        }
    }

    // This method is called when the associated ShedTrigger object is destroyed
    public void OnShedTriggerDestroyed()
    {
        if (triggerObject != null && !triggerObject.activeSelf)
        {
            Debug.Log("ShedTrigger destroyed, enabling triggerObject in ShedNoConcern.");
            triggerObject.SetActive(true);
        }
    }

    // This method is called when the player collides with the trigger object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the trigger object in shed.");
            hasTriggered = true; // Mark the shed as triggered

            // Enable the trigger object
            if (triggerObject != null && !triggerObject.activeSelf)
            {
                triggerObject.SetActive(true);
            }

            // Destroy the gameObject this script is attached to after triggering the dialogue
            Destroy(gameObject);
        }
    }
}
