using UnityEngine;

public class PostFlashlightTensionAudio : MonoBehaviour
{
    [SerializeField] private AudioClip tensionAudio; // The audio clip to play
    [SerializeField] private AudioSource audioSource; // Reference to the audio source

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the trigger
        if (other.CompareTag("Player"))
        {
            PlayTensionAudio();
        }
    }

    private void PlayTensionAudio()
    {
        if (tensionAudio != null && audioSource != null)
        {
            audioSource.PlayOneShot(tensionAudio);
            Debug.Log("Tension audio played!");
        }
    }

    // Method to stop the tension audio
    public void StopTensionAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Tension audio stopped!");
        }
    }
}