using System.Collections;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public FriendJumpScare friendJumpScareScript; // Reference to the FriendJumpScare script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the FriendJumpScare script to start the jump scare sequence
            if (friendJumpScareScript != null)
            {
                StartCoroutine(friendJumpScareScript.JumpScareSequence());
            }
        }
    }
}
