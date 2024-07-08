using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector pizzaCutsceneDirector; // Reference to the PlayableDirector
    public GameObject thePlayer;
    public GameObject playerCamera; // Reference to the player camera
    public GameObject pizzaCam; // Reference to the pizza cam

    private void Start()
    {
        if (pizzaCam != null)
        {
            pizzaCam.SetActive(false); // Ensure the pizza cam is initially inactive
        }
    }

    public void TriggerPizzaEatingCutscene()
    {
        if (pizzaCutsceneDirector != null)
        {
            pizzaCutsceneDirector.Play();
        }
        else
        {
            Debug.LogError("PizzaCutsceneDirector is not assigned!");
        }

        if (pizzaCam != null)
        {
            pizzaCam.SetActive(true); // Activate the pizza cam
        }
        if (playerCamera != null)
        {
            playerCamera.SetActive(false); // Deactivate the player camera
        }

        StartCoroutine(PlayPizzaCutscene());
    }

    private IEnumerator PlayPizzaCutscene()
    {
        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = false;
        }

        // Wait until the PlayableDirector has finished playing
        while (pizzaCutsceneDirector.state == PlayState.Playing)
        {
            yield return null;
        }

        if (thePlayer != null)
        {
            thePlayer.GetComponent<PlayerMovement>().enabled = true;
        }
        if (playerCamera != null)
        {
            playerCamera.SetActive(true); // Reactivate the player camera
        }
        if (pizzaCam != null)
        {
            pizzaCam.SetActive(false); // Deactivate the pizza cam
        }
    }
}