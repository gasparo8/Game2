using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    // Reference to the EndOfGameManager script
    public EndOfGameManager endOfGameManager;

    // Reference to the walker GameObject
    public GameObject walker;

    // Reference to the BreakerBoxSwitch script
    public BreakerBoxSwitch breakerBoxSwitch;

    // Reference to the AudioSource for the new sound that will fade in
    public AudioSource endOfGamePianoAudio;

    // Reference to the Renderer of the trigger box
    private Renderer triggerRenderer;

    private void Start()
    {
        // Get the Renderer component of the trigger object
        triggerRenderer = GetComponent<Renderer>();
    }


    // This function is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached End Game Trigger");

            // Fade out the walker's sound using BreakerBoxSwitch
            if (breakerBoxSwitch != null)
            {
                breakerBoxSwitch.FadeOutWalkerSound(2f);  // Fade out over 2 seconds
            }

            // Trigger the end game cutscene logic
            endOfGameManager.TriggerEndGameCutscene();


            /*
            // Fade out the walker's sound using BreakerBoxSwitch
            if (breakerBoxSwitch != null)
            {
                breakerBoxSwitch.FadeOutWalkerSound(2f);  // Fade out over 2 seconds
            }
            */


            // Disable the walker GameObject
            if (walker != null)
            {
                walker.SetActive(false);
            }

            // Fade in the new audio
            if (endOfGamePianoAudio != null)
            {
                StartCoroutine(FadeInEndGamePianoAudio(endOfGamePianoAudio, 2f));  // Fade in over 2 seconds
            }

            // Disable the renderer to make the trigger box invisible
            if (triggerRenderer != null)
            {
                triggerRenderer.enabled = false;
            }
        }
    }

    // Coroutine to fade in the audio over time
    private IEnumerator FadeInEndGamePianoAudio(AudioSource audioSource, float fadeDuration)
    {
        float targetVolume = audioSource.volume;  // The volume to fade in to
        audioSource.volume = 0f;  // Start with a volume of 0
        audioSource.Play();  // Start playing the audio

        float currentTime = 0f;
        
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeDuration);  // Gradually increase the volume
            yield return null;  // Wait until the next frame
        }

        audioSource.volume = targetVolume;  // Make sure the volume is set to the target at the end
    }
}
