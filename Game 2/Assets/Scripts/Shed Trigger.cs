using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedTrigger : MonoBehaviour
{
    public FriendJumpScare friendJumpScareScript; // Reference to the FriendJumpScare script
    public GameObject triggerObject; // The object you want to enable (the shed trigger)
    public LightFlicker lightFlicker; // Reference to the LightFlicker script
    public Dialogue dialogue;

    private void Start()
    {
        // Ensure the trigger object is disabled at the start
        if (triggerObject != null)
        {
            triggerObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the jump scare cutscene has been triggered
        if (friendJumpScareScript.jumpScareTriggered)
        {
            // Enable the trigger object if it's not already enabled
            if (triggerObject != null && !triggerObject.activeSelf)
            {
                triggerObject.SetActive(true);
            }
        }
    }

    // This method is called when the player collides with the trigger object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the trigger object.");
            lightFlicker.StopFlickering(); // Stop the light flickering

            // Destroy the gameObject this script is attached to after triggering the dialogue
            Destroy(gameObject);
        }
    }
}
