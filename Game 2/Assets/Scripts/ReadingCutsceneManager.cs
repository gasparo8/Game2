using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReadingCutsceneManager : MonoBehaviour
{
    public PlayableDirector readingCutsceneDirector;
    public GameObject thePlayer;
    public GameObject readingCam;
    public GameObject playerCam;
    public GameObject openBook;
    public GameObject closedBook;


    // Start is called before the first frame update
    void Start()
    {
        if (readingCam != null)
        {
            readingCam.SetActive(false); // Ensure the reading cam is initially inactive
        }

        openBook.SetActive(false);
    }

    public void TriggerReadingCutscene()
    {
        if (readingCutsceneDirector != null)
        {
            openBook.SetActive(true);
            readingCutsceneDirector.Play();
            Debug.Log("Reading Cutscene Played");
        }

        if (readingCam != null)
        {
            readingCam.SetActive(true); // Activate the pizza cam
        }

        if (playerCam != null)
        {
            playerCam.SetActive(false); // Deactivate the player camera
        }

        StartCoroutine(PlayReadingCutscene());
    }

    private IEnumerator PlayReadingCutscene()
    {
        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = false;
        }

        // Wait until the PlayableDirector has finished playing
        while (readingCutsceneDirector.state == PlayState.Playing)
        {
            yield return null;
        }
        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = true;
        }
        if (playerCam != null)
        {
            playerCam.SetActive(true); // Reactivate the player camera
        }
        if (readingCam != null)
        {
            readingCam.SetActive(false);
        }
        if (closedBook != null)
        {
            Destroy(closedBook);
        }
        if (openBook != null)
        {
            Destroy(openBook);
        }
    }
}