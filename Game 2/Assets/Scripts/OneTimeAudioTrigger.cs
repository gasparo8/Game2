using UnityEngine;
using System.Collections;

public class OneTimeAudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayAudioAndDestroy());
        }
    }

    private IEnumerator PlayAudioAndDestroy()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        Destroy(gameObject);
    }
}
