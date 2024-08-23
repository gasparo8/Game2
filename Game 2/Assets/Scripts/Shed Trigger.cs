using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedTrigger : MonoBehaviour
{
    public FriendJumpScare friendJumpScareScript; // Reference to the FriendJumpScare script
    public GameObject triggerObject; // The object you want to enable
    public LightFlicker lightFlicker; // Reference to the LightFlicker script

    void Update()
    {
        // Check if the jump scare cutscene has been triggered
        if (friendJumpScareScript.jumpScareTriggered)
        {
            // Enable the trigger object
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
        }
    }
}
