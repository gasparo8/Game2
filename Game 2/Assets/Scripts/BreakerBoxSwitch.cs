using UnityEngine;
using UnityEngine.AI;

public class BreakerBoxSwitch : MonoBehaviour
{
    public GameObject walker;  // Reference the existing walker in the scene
    public AudioClip switchSound;
    private AudioSource audioSource;
    private bool isSwitched = false;

    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Make sure the walker is disabled at the start
        if (walker != null)
        {
            walker.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength, layerMaskInteract))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    OnSwitchClicked();
                }
            }
        }
    }

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

            // Enable the existing walker instead of instantiating a new one
            if (walker != null)
            {
                walker.SetActive(true);
            }
        }
    }
}
