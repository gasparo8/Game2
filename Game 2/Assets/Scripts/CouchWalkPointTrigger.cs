using UnityEngine;

public class CouchWalkPointTrigger : MonoBehaviour
{
    public ReadingSecondCutsceneManager cutsceneManager; // Reference to the cutscene manager

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            cutsceneManager.TriggerSecondReadingCutscene(); // Call the cutscene manager to play the cutscene
        }
    }
}
