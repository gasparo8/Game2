using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SingleActionRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskInteract; // Ensure this includes the layers for both light switches and TV remote
    [SerializeField] private string excludeLayerName = null;

    private SpinController rayCastedTP;
    private LightSwitch rayCastedLightSwitch; // Reference to the LightSwitch script
    private TVRemote rayCastedTVRemote; // Reference to the TVRemote script
    private FlashlightController rayCastedFlashlight;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode pickUpKey = KeyCode.E;
    [SerializeField] private KeyCode placeKey = KeyCode.R;
    [SerializeField] private KeyCode toggleLightKey = KeyCode.Mouse0; // Key to toggle lights and TV

    [SerializeField] private Image crosshair = null;

    private bool isCrossHairActive;
    private bool doOnce;

    private const string interactableTag = "SingleActionInteraction";
    private const string pickableTag = "Pickable";
    private const string lightSwitchTag = "LightSwitch"; // Tag for light switches
    private const string pizzaBoxName = "PizzaBox"; // Name of the pizza box object
    private const string bookName = "Book"; // Name of the book object
    private const string dropZoneTag = "DropZone"; // Tag for the drop zone
    private const string bookWalkPointTag = "BookWalkPoint"; // Tag for the book walk point
    private const string tvRemoteTag = "TVRemote"; // Tag for TV Remote
    private const string flashlightTag = "Flashlight"; // New tag for flashlight
    private const string donutTag = "Donut"; // New tag for donut
    private const string knifeBlock = "KnifeBlock"; // New tag for KnifeBlock
    private const string coffeeMachine = "CoffeeMachine"; // New tag for CoffeeMachine
    private const string showerHandle = "ShowerHandle"; // New tag for ShowerHandle
    private const string fakePlant = "FakePlant"; // New tag for FakePlant
    private const string tissueBox = "TissueBox"; // Mew tag for Tissue Box
    private const string doorBell = "DoorBell";
    private const string guitar = "Guitar";
    private const string toothbrush = "Toothbrush";
    private const string blackBox = "Blackbox";

    private GameObject pickedUpObject = null;
    [SerializeField] private Transform holdPoint; // Point where the object will be held
    [SerializeField] private GameObject glowingBox; // Reference to the glowing box
    public GameObject thePlayer;
    public CutsceneManager cutsceneManager; // Reference to the CutsceneManager
    public GameObject pizzaNBoxOpen; // Reference to PizzaNBoxOPEN object
    public GameObject bookWalkPoint; // Reference to the BookWalkPoint object
    public ReadingCutsceneManager readingCutsceneManager;
    public BookDialogueTrigger bookDialogueTrigger;

    private void Start()
    {
        glowingBox.SetActive(false); // Ensure the glowing box is initially invisible
        if (pizzaNBoxOpen != null)
        {
            pizzaNBoxOpen.SetActive(false); // Ensure PizzaNBoxOPEN is initially inactive
        }
        if (bookWalkPoint != null)
        {
            bookWalkPoint.SetActive(false); // Ensure the BookWalkPoint is initially inactive
        }

    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // Layer mask to include the layers for interaction
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    rayCastedTP = hit.collider.gameObject.GetComponent<SpinController>();
                    CrosshairChange(true);
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    // rayCastedTP.PlayTPAnimation();
                    if (rayCastedTP != null)
                    {
                        rayCastedTP.PlayTPAnimation();
                    }
                }
            }
            else if (hit.collider.CompareTag(pickableTag))
            {
                if (!doOnce)
                {
                    CrosshairChange(true);
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(pickUpKey) && pickedUpObject == null)
                {
                    pickedUpObject = hit.collider.gameObject;
                    PickUpObject(pickedUpObject);
                }
            }
            else if (hit.collider.CompareTag(lightSwitchTag))
            {
                if (!doOnce)
                {
                    rayCastedLightSwitch = hit.collider.gameObject.GetComponent<LightSwitch>();
                    CrosshairChange(true);
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey))
                {
                    rayCastedLightSwitch.ToggleLights(); // Toggle the lights
                }
            }
            else if (hit.collider.CompareTag(tvRemoteTag)) // New logic for TV Remote
            {
                if (!doOnce)
                {
                    rayCastedTVRemote = hit.collider.gameObject.GetComponent<TVRemote>();
                    CrosshairChange(true); // Change crosshair on hover over remote
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Same key to toggle TV
                {
                    rayCastedTVRemote.ToggleTV(); // Toggle TV on/off
                }
            }

            else if (hit.collider.CompareTag(flashlightTag)) // Logic for Flashlight click
            {
                if (!doOnce)
                {
                    rayCastedFlashlight = hit.collider.gameObject.GetComponent<FlashlightController>();
                    CrosshairChange(true); // Change crosshair on hover over remote
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(pickUpKey))
                {
                    rayCastedFlashlight.EnableFlashlight(); // Call method to enable flashlight
                }
            }

            else if (hit.collider.CompareTag(coffeeMachine)) // Detect the coffeeMachine
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    // Play the audio clip attached to the knifeBlock object
                    AudioSource audioSource = hit.collider.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play(); // Play the audio clip
                    }

                    // Trigger the dialogue if available
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                }
            }

            else if (hit.collider.CompareTag(tissueBox)) // Detect the coffeeMachine
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Clicked on tissue box
                {
                    TissueBoxInteraction tissue = hit.collider.GetComponent<TissueBoxInteraction>();
                    if (tissue != null)
                    {
                        tissue.PlayRandomTissueSound();
                    }
                }
            }

            else if (hit.collider.CompareTag(knifeBlock)) // Detect the knifeBlock
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                    else
                    {
                        Debug.LogWarning("DialogueTrigger not found on the knifeBlock object!");
                    }
                }
            }

            else if (hit.collider.CompareTag(fakePlant)) // Detect the fakePlant
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                    else
                    {
                        Debug.LogWarning("DialogueTrigger not found on the fakePlant object!");
                    }
                }
            }


            else if (hit.collider.CompareTag(blackBox)) // Detect the blackbox
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                    else
                    {
                        Debug.LogWarning("DialogueTrigger not found on the blackbox object!");
                    }
                }
            }

            else if (hit.collider.CompareTag(showerHandle)) // Detect the showerHandle
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                    else
                    {
                        Debug.LogWarning("DialogueTrigger not found on the showerHandle object!");
                    }
                }
            }

            else if (hit.collider.CompareTag(doorBell)) // Detect the doorBell
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    // Play the audio clip attached to the doorbell object
                    AudioSource audioSource = hit.collider.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play(); // Play the audio clip
                    }

                }
            }

            else if (hit.collider.CompareTag(guitar)) // Detect the doorBell
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    // Play the audio clip attached to the doorbell object
                    AudioSource audioSource = hit.collider.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play(); // Play the audio clip
                    }

                }
            }

            else if (hit.collider.CompareTag(toothbrush)) // Detect the doorBell
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    // Play the audio clip attached to the doorbell object
                    AudioSource audioSource = hit.collider.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play(); // Play the audio clip
                    }

                }
            }

            else if (hit.collider.CompareTag(donutTag)) // Detect the donut
            {
                if (!doOnce)
                {
                    CrosshairChange(true); // Highlight the crosshair
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(toggleLightKey)) // Use the same key as other interactions
                {
                    DialogueTrigger dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.TriggerDialogue(); // Trigger the dialogue
                    }
                    else
                    {
                        Debug.LogWarning("DialogueTrigger not found on the donut object!");
                    }
                }
            }
        }

        else
        {
            if (isCrossHairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }

        if (pickedUpObject != null && Input.GetKeyDown(placeKey))
        {
            PlaceObject(pickedUpObject);
        }
    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrossHairActive = false;
        }
    }

    void PickUpObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
        obj.transform.SetParent(holdPoint); // Attach to hold point
        obj.transform.localPosition = Vector3.zero; // Reset position relative to hold point

        if (obj.name == pizzaBoxName) // Check if the object is the pizza box
        {
            glowingBox.SetActive(true); // Show the glowing box when the pizza box is picked up
        }
        else if (obj.name == bookName) // Check if the object is the book
        {
            bookDialogueTrigger.BookToCouchDialogue();

            if (bookWalkPoint != null)
            {
                bookWalkPoint.SetActive(true); // Activate the BookWalkPoint when the book is picked up
            }
        }
    }

    void PlaceObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
        obj.transform.SetParent(null); // Detach from hold point
        pickedUpObject = null; // Clear reference to the picked up object

        if (obj.name == pizzaBoxName)
        {
            // Check if the pizza box is within the drop zone when placed
            Collider[] colliders = Physics.OverlapSphere(obj.transform.position, 1.0f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(dropZoneTag))
                {
                    glowingBox.SetActive(false); // Hide the glowing box

                    cutsceneManager.TriggerPizzaEatingCutscene();

                    // Activate PizzaNBoxOPEN object
                    if (pizzaNBoxOpen != null)
                    {
                        pizzaNBoxOpen.SetActive(true);
                    }

                    // Destroy pizza box
                    Destroy(obj);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bookWalkPointTag) && pickedUpObject != null && pickedUpObject.name == bookName)
        {
            Debug.Log("Player has entered the Book Walk Point with the book.");
            // Here, you can trigger a cutscene or any other action you want

            bookWalkPoint.SetActive(false);

            // Trigger the reading cutscene
            if (readingCutsceneManager != null)
            {
                readingCutsceneManager.TriggerReadingCutscene();
            }
        }
    }
}
