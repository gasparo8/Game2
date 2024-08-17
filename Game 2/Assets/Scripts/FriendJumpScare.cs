using System.Collections;
using UnityEngine;

public class FriendJumpScare : MonoBehaviour
{
    public AudioSource jumpScareAudio; // Reference to the jump scare audio
    public GameObject jumpScareCutscene; // Reference to the jump scare cutscene
    public GameObject player; // Reference to the player
    public GameObject playerCam; // Reference to the player's camera
    public GameObject jumpTrigger; // Reference to the jump trigger collider
    public LightFlicker lightsFlickerScript; // Reference to the LightFlicker script

    private PlayerMovement playerMovement; // Reference to the player's movement script

    private void Start()
    {
        if (jumpTrigger != null)
        {
            jumpTrigger.SetActive(false); // Ensure the jump trigger is disabled at the start
        }

        // Get the player's movement script
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        // Check if light flickering has started and enable the jump trigger
        if (lightsFlickerScript != null && jumpTrigger != null)
        {
            if (lightsFlickerScript.IsFlickering())
            {
                jumpTrigger.SetActive(true); // Enable the jump trigger when flickering starts
            }
        }
    }

    public IEnumerator JumpScareSequence()
    {
        // Disable player movement but keep the camera active
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement
        }

        // Activate the jump scare cutscene
        if (jumpScareCutscene != null)
        {
            jumpScareCutscene.SetActive(true);
        }

        // Play the jump scare audio within the cutscene
        if (jumpScareAudio != null)
        {
            jumpScareAudio.Play();
        }

        // Wait for the cutscene and audio to complete
        yield return new WaitForSeconds(jumpScareAudio.clip.length);

        // Deactivate the jump scare cutscene
        if (jumpScareCutscene != null)
        {
            jumpScareCutscene.SetActive(false);
        }

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Disable the jump trigger after the scare
        if (jumpTrigger != null)
        {
            jumpTrigger.SetActive(false);
        }
    }
}
