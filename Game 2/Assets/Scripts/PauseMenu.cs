using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public MouseLook mouseLook;

    [Header("Button Sound & Color Settings")]
    public AudioSource audioSource;
    public AudioClip highlightSound;
    public AudioClip pressedSound;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;

    [Header("Font Size Settings")]
    public int normalFontSize = 36;
    public int hoverFontSize = 42;

    [Header("Mouse Sensitivity Settings")]
    public Slider mouseSensitivitySlider;    // assign this in the Inspector
    public float defaultSensitivity = 40f;   // midpoint value
    public float sensitivityRange = 30f;     // total range (above & below default)
    public string prefsKey = "MouseSensitivity";

    private List<Button> pauseButtons = new List<Button>();

    void Start()
    {
        // --- Button setup ---
        if (pauseMenuUI != null)
            pauseButtons.AddRange(pauseMenuUI.GetComponentsInChildren<Button>(true));

        foreach (var button in pauseButtons)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText == null) continue;

            // Set default font size
            buttonText.fontSize = normalFontSize;

            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = button.gameObject.AddComponent<EventTrigger>();

            // Pointer Enter
            EventTrigger.Entry enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener((_) =>
            {
                PlaySound(highlightSound);
                ChangeTextAppearance(buttonText, hoverColor, hoverFontSize);
            });
            trigger.triggers.Add(enter);

            // Pointer Exit
            EventTrigger.Entry exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            exit.callback.AddListener((_) => ChangeTextAppearance(buttonText, normalColor, normalFontSize));
            trigger.triggers.Add(exit);

            // Click sound
            button.onClick.AddListener(() => PlaySound(pressedSound));
        }

        // --- Mouse Sensitivity Slider setup ---
        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.minValue = defaultSensitivity - sensitivityRange / 2f;
            mouseSensitivitySlider.maxValue = defaultSensitivity + sensitivityRange / 2f;

            if (PlayerPrefs.HasKey(prefsKey))
            {
                float saved = PlayerPrefs.GetFloat(prefsKey);
                mouseSensitivitySlider.SetValueWithoutNotify(saved);
                if (mouseLook != null)
                    mouseLook.mouseSensitivity = saved;
            }
            else
            {
                float middleValue = (mouseSensitivitySlider.minValue + mouseSensitivitySlider.maxValue) / 2f;
                mouseSensitivitySlider.SetValueWithoutNotify(middleValue);
                if (mouseLook != null)
                    mouseLook.mouseSensitivity = middleValue;
            }

            mouseSensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

            EventTrigger sliderTrigger = mouseSensitivitySlider.gameObject.GetComponent<EventTrigger>();
            if (sliderTrigger == null)
                sliderTrigger = mouseSensitivitySlider.gameObject.AddComponent<EventTrigger>();

            TMP_Text sliderText = mouseSensitivitySlider.GetComponentInChildren<TMP_Text>();
            if (sliderText != null)
                sliderText.fontSize = normalFontSize;

            // Pointer Enter
            EventTrigger.Entry sliderEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            sliderEnter.callback.AddListener((_) =>
            {
                PlaySound(highlightSound);
                ChangeTextAppearance(sliderText, hoverColor, hoverFontSize);
            });
            sliderTrigger.triggers.Add(sliderEnter);

            // Pointer Exit
            EventTrigger.Entry sliderExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            sliderExit.callback.AddListener((_) => ChangeTextAppearance(sliderText, normalColor, normalFontSize));
            sliderTrigger.triggers.Add(sliderExit);
        }
        else
        {
            Debug.LogWarning("PauseMenu: MouseSensitivitySlider not assigned in Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        mouseLook.LockCursor();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        mouseLook.UnlockCursor();

        if (mouseSensitivitySlider != null && mouseLook != null)
            mouseSensitivitySlider.SetValueWithoutNotify(mouseLook.mouseSensitivity);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Scene");
    }

    private void OnSensitivityChanged(float newValue)
    {
        if (mouseLook != null)
            mouseLook.mouseSensitivity = newValue;

        PlayerPrefs.SetFloat(prefsKey, newValue);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        if (mouseSensitivitySlider != null)
            mouseSensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
    }

    // --- Helper methods for sound, color & font size ---
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    private void ChangeTextAppearance(TMP_Text text, Color color, int fontSize)
    {
        if (text != null)
        {
            text.color = color;
            text.fontSize = fontSize;
        }
    }
}
