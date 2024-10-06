using UnityEngine;
using UnityEngine.AI;  // For NavMesh functionality
using System.Collections;
using System.Collections.Generic;

public class BreakerBoxSwitch : MonoBehaviour
{
    public GameObject mixamoCharacterPrefab;  // Assign your Mixamo character prefab here
    public Transform spawnLocation;  // Assign the transform where the Mixamo character will appear
    public AudioClip switchSound;  // Assign the switch sound audio clip
    private AudioSource audioSource;
    private bool isSwitched = false;

    [SerializeField] private int rayLength = 4; // The length of the ray for interaction
    [SerializeField] private LayerMask layerMaskInteract; // Layer for interactable objects
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0; // Key to interact with the breaker box

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey)) // Detect left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength, layerMaskInteract))
            {
                if (hit.collider.gameObject == gameObject) // If the breaker box is clicked
                {
                    OnSwitchClicked();
                }
            }
        }
    }

    // This function is called when the player clicks on the switch
    public void OnSwitchClicked()
    {
        if (!isSwitched)
        {
            isSwitched = true;

            // Play the switch sound
            if (switchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(switchSound);
            }

            // Instantiate the Mixamo character at the specified spawn location
            if (mixamoCharacterPrefab != null && spawnLocation != null)
            {
                Instantiate(mixamoCharacterPrefab, spawnLocation.position, spawnLocation.rotation);
            }
        }
    }
}
