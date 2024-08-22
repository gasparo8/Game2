using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shed : MonoBehaviour
{
    public FriendJumpScare friendJumpScareScript; // Reference to the FriendJumpScare script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the cutscene has been triggered
        if (friendJumpScareScript.jumpScareTriggered)
        {
            // Handle logic based on the cutscene completion
        }
    }
}
