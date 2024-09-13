using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVRemote : MonoBehaviour
{
    public VideoPlayer tvVideoPlayer; // Assign the TV's VideoPlayer in the Inspector
    public AudioSource tvAudioSource; // Assign the TV's AudioSource in the Inspector
    public GameObject tvScreen; // The screen of the TV (the quad)

    private bool isTVOn = false; // State of the TV (on/off)

    void Start()
    {
        tvScreen.SetActive(false); // Ensure the TV screen is disabled at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Detect left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // If the remote control is clicked
                {
                    ToggleTV();
                }
            }
        }
    }

    public void ToggleTV()
    {  
        isTVOn = !isTVOn; // Toggle the TV state

        if (isTVOn)
        {
            tvScreen.SetActive(true); // Enable the TV screen
            tvVideoPlayer.Play(); // Play the video
            tvAudioSource.Play(); // Play the audio
        }
        else
        {
            tvVideoPlayer.Pause(); // Pause the video
            tvAudioSource.Pause(); // Pause the audio
            tvScreen.SetActive(false); // Disable the TV screen
        }
    }
}