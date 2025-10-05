using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ButtonSoundManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonSound
    {
        public Button button;            // Reference to the button
        public AudioClip highlightSound; // Highlight sound
        public AudioClip pressedSound;   // Pressed sound
        public TMP_Text buttonText;      // TextMeshPro text on the button
        public float fontSizeIncrease = 2f; // Amount to increase font size
        public Color normalColor = Color.white; // Default color
        public Color hoverColor = Color.yellow; // Hover color
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

        foreach (var buttonSound in buttonSounds)
        {
            if (buttonSound.button != null && buttonSound.buttonText != null)
            {
                EventTrigger trigger = buttonSound.button.gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                    trigger = buttonSound.button.gameObject.AddComponent<EventTrigger>();

                // --- Pointer Enter (hover) ---
                EventTrigger.Entry enter = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter
                };
                enter.callback.AddListener((_) =>
                {
                    PlaySound(buttonSound.highlightSound);
                    AdjustFontSize(buttonSound.buttonText, buttonSound.fontSizeIncrease);
                    ChangeTextColor(buttonSound.buttonText, buttonSound.hoverColor);
                });
                trigger.triggers.Add(enter);

                // --- Pointer Exit ---
                EventTrigger.Entry exit = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerExit
                };
                exit.callback.AddListener((_) =>
                {
                    AdjustFontSize(buttonSound.buttonText, -buttonSound.fontSizeIncrease);
                    ChangeTextColor(buttonSound.buttonText, buttonSound.normalColor);
                });
                trigger.triggers.Add(exit);

                // --- Pointer Click (pressed) ---
                buttonSound.button.onClick.AddListener(() =>
                {
                    PlaySound(buttonSound.pressedSound);
                    // Optional: temporarily increase font size on click
                    AdjustFontSize(buttonSound.buttonText, buttonSound.fontSizeIncrease);
                });
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    private void AdjustFontSize(TMP_Text text, float adjustment)
    {
        if (text != null)
            text.fontSize += adjustment;
    }

    private void ChangeTextColor(TMP_Text text, Color color)
    {
        if (text != null)
            text.color = color;
    }
}
