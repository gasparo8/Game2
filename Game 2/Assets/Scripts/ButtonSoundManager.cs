using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSoundManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonSound
    {
        public Button button;           // Reference to the button
        public AudioClip highlightSound; // Highlight sound
        public AudioClip pressedSound;  // Pressed sound
    }

    public List<ButtonSound> buttonSounds; // List of buttons and their sounds
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the GameObject. Please add one!");
            return;
        }

        // Add event listeners for all buttons in the list
        foreach (var buttonSound in buttonSounds)
        {
            if (buttonSound.button != null)
            {
                EventTrigger trigger = buttonSound.button.gameObject.AddComponent<EventTrigger>();

                // Add OnPointerEnter (highlight)
                EventTrigger.Entry highlightEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter
                };
                highlightEntry.callback.AddListener((_) => PlaySound(buttonSound.highlightSound));
                trigger.triggers.Add(highlightEntry);

                // Add OnPointerClick (pressed)
                buttonSound.button.onClick.AddListener(() => PlaySound(buttonSound.pressedSound));
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
