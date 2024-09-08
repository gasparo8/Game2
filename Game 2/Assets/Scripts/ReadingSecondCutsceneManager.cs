using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReadingSecondCutsceneManager : MonoBehaviour
{
    public PlayableDirector readingSecondCutsceneDirector;
    public GameObject thePlayer;
    public GameObject playerCam;
    public GameObject openBook;
    public GameObject readingCamera;
    public GameObject couchWalkPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (readingCamera != null)
        {
            readingCamera.SetActive(false); // Ensure the reading cam is initially inactive
        }

        if (couchWalkPoint != null)
        {
            couchWalkPoint.SetActive(false); // Ensure the reading cam is initially inactive
        }
    }

    public void TriggerSecondReadingCutscene()
    {
        if (readingSecondCutsceneDirector != null)
        {
            openBook.SetActive(true);
            readingSecondCutsceneDirector.Play();
            Debug.Log("Second Reading Cutscene Played");
        }

        if (readingCamera != null)
        {
            readingCamera.SetActive(true); // Ensure the reading cam is initially inactive
        }

        if (playerCam != null)
        {
            playerCam.SetActive(false); // Deactivate the player camera
        }
    }
}
