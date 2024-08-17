using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.5f;
    public Renderer windowRenderer;

    private bool isFlickering = false;

    private void Start()
    {
        windowRenderer = GetComponent<Renderer>();
        StartCoroutine(FlickerLight()); // Start the flickering coroutine
    }

    // Coroutine to handle light flickering
    public IEnumerator FlickerLight()
    {
        isFlickering = true; // Set flickering flag to true

        while (true)
        {
            float flickerTime = Random.Range(minFlickerTime, maxFlickerTime);

            // Toggle between light on and off materials
            if (windowRenderer.material.name.Contains(lightOnMaterial.name))
            {
                windowRenderer.material = lightOffMaterial;
                Debug.Log("Light Off");
            }
            else
            {
                windowRenderer.material = lightOnMaterial;
                Debug.Log("Light On");
            }

            yield return new WaitForSeconds(flickerTime);
        }
    }

    // Method to check if light is flickering
    public bool IsFlickering()
    {
        return isFlickering;
    }
}
