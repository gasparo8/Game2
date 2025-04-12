using UnityEngine;

public class TissueBoxInteraction : MonoBehaviour
{
    public AudioClip[] tissuePullSounds;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomTissueSound()
    {
        if (tissuePullSounds.Length == 0 || audioSource == null)
            return;

        AudioClip randomClip = tissuePullSounds[Random.Range(0, tissuePullSounds.Length)];
        audioSource.PlayOneShot(randomClip);
    }
}