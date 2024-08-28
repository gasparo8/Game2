using UnityEngine;

public class PlayAudioOnEvent : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
