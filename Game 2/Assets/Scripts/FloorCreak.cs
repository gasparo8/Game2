using UnityEngine;
using System.Collections;

public class FloorCreak : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip[] creakSounds; // Array to hold creak sounds
    public AudioSource audioSource; // Reference to AudioSource component

    [Header("Cooldown Settings")]
    [Tooltip("Cooldown period before the trigger can be activated again.")]
    public float cooldownTime = 10f;

    private bool isCooldown = false; // To track cooldown state

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCooldown)
        {
            PlayCreakSound();
        }
    }

    private void PlayCreakSound()
    {
        if (creakSounds.Length > 0)
        {
            // Play a random creak sound
            int randomIndex = Random.Range(0, creakSounds.Length);
            audioSource.PlayOneShot(creakSounds[randomIndex]);
        }

        // Start the cooldown
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
