using UnityEngine;
using System.Collections;

public class OwlHootTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource owlAudioSource;
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 5f;
    [SerializeField] private float pitchMin = 0.95f;
    [SerializeField] private float pitchMax = 1.05f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(PlayHootAndDestroy());
        }
    }

    private IEnumerator PlayHootAndDestroy()
    {
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);

        owlAudioSource.pitch = Random.Range(pitchMin, pitchMax);
        owlAudioSource.Play();

        // Wait for clip to finish before destroying object
        yield return new WaitForSeconds(owlAudioSource.clip.length);
        Destroy(gameObject);
    }
}
