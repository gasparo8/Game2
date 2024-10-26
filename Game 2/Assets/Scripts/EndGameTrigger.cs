using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    // Reference to the EndOfGameManager script
    public EndOfGameManager endOfGameManager;

    // Reference to the walker GameObject
    public GameObject walker;

    // This function is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached End Game Trigger");

            // Trigger the end game cutscene logic
            endOfGameManager.TriggerEndGameCutscene();

            // Disable the walker GameObject
            if (walker != null)
            {
                walker.SetActive(false);
            }

            // Destroy the game object (this trigger box) after the player enters
            Destroy(gameObject);
        }
    }
}