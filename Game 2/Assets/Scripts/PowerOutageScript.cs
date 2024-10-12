using System.Collections.Generic;
using UnityEngine;

public class PowerOutageScript : MonoBehaviour
{
    [System.Serializable]
    public class RendererMaterials
    {
        public Renderer objectRenderer;  // The renderer of the light object
        public Material onMaterial;      // Material for when the light is on
        public Material offMaterial;     // Material for when the light is off
    }

    [System.Serializable]
    public class LightObject
    {
        public Light lightSource;  // The light component (can be null if no real light)
        public List<RendererMaterials> renderers = new List<RendererMaterials>();  // List of renderers with materials to swap
    }

    public List<LightObject> lights = new List<LightObject>();  // List of lights and their corresponding renderers/materials

    [SerializeField]
    private bool triggerPowerOutage = false;  // Checkbox to test power outage
    [SerializeField]
    private bool restorePower = false;  // Checkbox to test restoring power

    private bool powerOut = false;  // Track the current power state

    private void Update()
    {
        // Check the checkbox during testing for triggering power outage
        if (triggerPowerOutage && !powerOut)
        {
            TriggerOutage();
            triggerPowerOutage = false;  // Reset the checkbox
        }

        // Check the checkbox during testing for restoring power
        if (restorePower && powerOut)
        {
            ResetPower();
            restorePower = false;  // Reset the checkbox
        }
    }

    // Method to trigger the power outage
    public void TriggerOutage()
    {
        powerOut = true;
        LightSwitch.isPowerOut = true;  // Update the global flag

        foreach (LightObject lightObj in lights)
        {
            // Turn off the light
            if (lightObj.lightSource != null)
            {
                lightObj.lightSource.enabled = false;
            }

            // Swap materials for each renderer
            foreach (RendererMaterials rendererMaterial in lightObj.renderers)
            {
                if (rendererMaterial.objectRenderer != null && rendererMaterial.offMaterial != null)
                {
                    rendererMaterial.objectRenderer.material = rendererMaterial.offMaterial;
                }
            }
        }

        Debug.Log("Power outage triggered.");
    }

    // Method to reset power (restores lights and materials to 'on' state)
    public void ResetPower()
    {
        powerOut = false;
        LightSwitch.isPowerOut = false;  // Reset the global flag

        foreach (LightObject lightObj in lights)
        {
            // Turn on the light
            if (lightObj.lightSource != null)
            {
                lightObj.lightSource.enabled = true;
            }

            // Swap materials back to 'on' state
            foreach (RendererMaterials rendererMaterial in lightObj.renderers)
            {
                if (rendererMaterial.objectRenderer != null && rendererMaterial.onMaterial != null)
                {
                    rendererMaterial.objectRenderer.material = rendererMaterial.onMaterial;
                }
            }
        }

        Debug.Log("Power restored.");
    }
}
