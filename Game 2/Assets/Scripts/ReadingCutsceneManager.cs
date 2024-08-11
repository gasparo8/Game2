using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReadingCutsceneManager : MonoBehaviour
{
    public PlayableDirector readingCutsceneDirector;
    public GameObject thePlayer;
    public GameObject readingCam;
    public GameObject headDropCam;
    public GameObject playerCam;
    public GameObject openBook;
    public GameObject closedBook;
    public GameObject topEyelid;
    public GameObject bottomEyelid;


    // Start is called before the first frame update
    void Start()
    {
        if (readingCam != null)
        {
            readingCam.SetActive(false); // Ensure the reading cam is initially inactive
        }

        if (headDropCam != null)
        {
            headDropCam.SetActive(false); // Ensure the reading cam is initially inactive
        }

        if (topEyelid != null)
        {
            topEyelid.SetActive(false); // Ensure the topEyelid is initially inactive
        }

        if (bottomEyelid != null)
        {
            bottomEyelid.SetActive(false); // Ensure the bottomEyelid is initially inactive
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
            readingCam.SetActive(true); 
        }

        if (headDropCam != null)
        {
            headDropCam.SetActive(true); 
        }

        if (playerCam != null)
        {
            playerCam.SetActive(false); // Deactivate the player camera
        }

        if (topEyelid != null)
        {
            topEyelid.SetActive(true); 
        }

        if (bottomEyelid != null)
        {
            bottomEyelid.SetActive(true); 
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

        if (topEyelid != null)
        {
            Destroy(topEyelid);
        }

        if (bottomEyelid != null)
        {
            Destroy(bottomEyelid);
        }

        if (headDropCam != null)
        {
            Destroy(headDropCam);
        }
    }
}