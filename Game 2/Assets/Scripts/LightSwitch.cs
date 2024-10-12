/*using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] roomLights; // Array of lights in the room to be controlled by the switch
    public Renderer[] lightRenderers; // Array of renderers for each light object
    public Material onMaterial; // Material to use when the light is on
    public Material offMaterial; // Material to use when the light is off
    private bool lightsOn = true; // Track the state of the lights

    public AudioClip switchOnClip; // Audio clip for turning the light on
    public AudioClip switchOffClip; // Audio clip for turning the light off
    private AudioSource audioSource; // AudioSource component for playing audio clips

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleLights()
    {
        lightsOn = !lightsOn; // Toggle the lights state

        // Loop through each light in the room and set its state
        foreach (Light light in roomLights)
        {
            light.enabled = lightsOn;
        }

        // Change the materials of the light renderers based on the light state
        foreach (Renderer renderer in lightRenderers)
        {
            if (lightsOn)
            {
                renderer.material = onMaterial; // Set the on material
            }
            else
            {
                renderer.material = offMaterial; // Set the off material
            }
        }

        // Play the appropriate audio clip based on the light state
        if (lightsOn && switchOnClip != null)
        {
            audioSource.PlayOneShot(switchOnClip); // Play the switch on clip
        }
        else if (!lightsOn && switchOffClip != null)
        {
            audioSource.PlayOneShot(switchOffClip); // Play the switch off clip
        }
    }
}
*/
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] roomLights; // Array of lights in the room to be controlled by the switch
    public Renderer[] lightRenderers; // Array of renderers for each light object
    public Material onMaterial; // Material to use when the light is on
    public Material offMaterial; // Material to use when the light is off
    private bool lightsOn = true; // Track the state of the lights

    public AudioClip switchOnClip; // Audio clip for turning the light on
    public AudioClip switchOffClip; // Audio clip for turning the light off
    private AudioSource audioSource; // AudioSource component for playing audio clips

    public static bool isPowerOut = false;  // Static variable to track global power outage

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleLights()
    {
        // Prevent lights from toggling if power is out
        if (isPowerOut)
        {
            Debug.Log("Power is out! Cannot toggle lights.");
            return;
        }

        lightsOn = !lightsOn; // Toggle the lights state

        // Loop through each light in the room and set its state
        foreach (Light light in roomLights)
        {
            light.enabled = lightsOn;
        }

        // Change the materials of the light renderers based on the light state
        foreach (Renderer renderer in lightRenderers)
        {
            if (lightsOn)
            {
                renderer.material = onMaterial; // Set the on material
            }
            else
            {
                renderer.material = offMaterial; // Set the off material
            }
        }

        // Play the appropriate audio clip based on the light state
        if (lightsOn && switchOnClip != null)
        {
            audioSource.PlayOneShot(switchOnClip); // Play the switch on clip
        }
        else if (!lightsOn && switchOffClip != null)
        {
            audioSource.PlayOneShot(switchOffClip); // Play the switch off clip
        }
    }
}
