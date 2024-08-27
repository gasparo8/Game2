/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyRoomPEEK : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public GameObject peekerObject; // Reference to the FrontDoorPeeker GameObject
    private bool animationPlayed = false; // To ensure the animation plays only once

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        // Ensure the peekerObject is assigned
        if (peekerObject == null)
        {
            Debug.LogError("Peeker object not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player (use tags or layers)
        if (other.CompareTag("Player") && !animationPlayed)
        {
            // Set the trigger to play the animation
            anim.SetTrigger("PlayFamilyRoomPeek");
            animationPlayed = true; // Ensure it only plays once
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyRoomPEEK : MonoBehaviour
{
    public Animator anim; // Reference to the Animator component
    public GameObject peekerObject; // Reference to the FrontDoorPeeker GameObject
    private bool animationPlayed = false; // To ensure the animation plays only once
    public AudioClip audioClip; // Reference to the audio clip
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Ensure the Animator component is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        // Ensure the peekerObject is assigned
        if (peekerObject == null)
        {
            Debug.LogError("Peeker object not assigned!");
        }

        // Add or get the AudioSource component on the same GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the audio clip to the audio source
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else
        {
            Debug.LogError("Audio clip not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player (use tags or layers)
        if (other.CompareTag("Player") && !animationPlayed)
        {
            // Set the trigger to play the animation
            anim.SetTrigger("PlayFamilyRoomPeek");

            // Play the audio clip
            if (audioSource != null && audioClip != null)
            {
                audioSource.Play();
            }

            animationPlayed = true; // Ensure it only plays once
        }
    }
}
