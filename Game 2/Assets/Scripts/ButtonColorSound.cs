using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro; // Include TextMeshPro namespace

public class ButtonColorSound : MonoBehaviour

{
    [System.Serializable]
    public class ButtonSound
    {
        public Button button;           // Reference to the button
        public AudioClip highlightSound; // Highlight sound
        public AudioClip pressedSound;  // Pressed sound
        public TMP_Text buttonText;     // TextMeshPro text on the button
        public Color normalColor = Color.white; // Default color of the text
        public Color hoverColor = Color.yellow; // Color to change to on hover
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
            if (buttonSound.button != null && buttonSound.buttonText != null)
            {
                EventTrigger trigger = buttonSound.button.gameObject.AddComponent<EventTrigger>();

                // Add OnPointerEnter (highlight)
                EventTrigger.Entry highlightEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter
                };
                highlightEntry.callback.AddListener((_) =>
                {
                    PlaySound(buttonSound.highlightSound);
                    ChangeTextColor(buttonSound.buttonText, buttonSound.hoverColor);
                });
                trigger.triggers.Add(highlightEntry);

                // Add OnPointerExit (reset text color)
                EventTrigger.Entry exitEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerExit
                };
                exitEntry.callback.AddListener((_) => ChangeTextColor(buttonSound.buttonText, buttonSound.normalColor));
                trigger.triggers.Add(exitEntry);

                // Add OnPointerClick (pressed)
                buttonSound.button.onClick.AddListener(() =>
                {
                    PlaySound(buttonSound.pressedSound);
                });
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

    private void ChangeTextColor(TMP_Text text, Color color)
    {
        if (text != null)
        {
            text.color = color;
        }
    }
}