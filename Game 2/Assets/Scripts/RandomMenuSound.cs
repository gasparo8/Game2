using UnityEngine;

public class RandomMenuSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float minDelay = 0f;
    public float maxDelay = 10f;

    private void Start()
    {
        if (audioSource != null)
            Invoke(nameof(PlaySound), Random.Range(minDelay, maxDelay));
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
