using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoShedTaskAudio : MonoBehaviour
{
    private AudioSource audioSource;

    // Singleton instance for easy reference from other scripts
    public static PianoShedTaskAudio Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Play the piano key task audio
    public void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Stop the piano key task audio
    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
// This audio is from virtual piano website and is just B then C