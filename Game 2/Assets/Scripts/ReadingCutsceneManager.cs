using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReadingCutsceneManager : MonoBehaviour
{
    public PlayableDirector readingCutsceneDirector;
    public GameObject thePlayer;
    public GameObject readingCam;

    // Start is called before the first frame update
    void Start()
    {
        if (readingCam != null)
        {
            readingCam.SetActive(false); // Ensure the reading cam is initially inactive
        }
    }
    public void TriggerReadingCutscene()
    {
        if (readingCutsceneDirector != null)
        {
            readingCutsceneDirector.Play();
            Debug.Log("Reading Cutscene Played");
        }

        if (readingCam != null)
        {
            readingCam.SetActive(true); // Activate the pizza cam
        }
    }
}
