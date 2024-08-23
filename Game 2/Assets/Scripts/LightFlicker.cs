/*using System.Collections;
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
            }
            else
            {
                windowRenderer.material = lightOnMaterial;
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
*/
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
    private Coroutine flickerCoroutine;

    private void Start()
    {
        windowRenderer = GetComponent<Renderer>();
        flickerCoroutine = StartCoroutine(FlickerLight()); // Start the flickering coroutine
    }

    // Coroutine to handle light flickering
    public IEnumerator FlickerLight()
    {
        isFlickering = true; // Set flickering flag to true

        while (isFlickering)
        {
            float flickerTime = Random.Range(minFlickerTime, maxFlickerTime);

            // Toggle between light on and off materials
            if (windowRenderer.material.name.Contains(lightOnMaterial.name))
            {
                windowRenderer.material = lightOffMaterial;
            }
            else
            {
                windowRenderer.material = lightOnMaterial;
            }

            yield return new WaitForSeconds(flickerTime);
        }
    }

    // Method to keep the light on by stopping the flickering
    public void StopFlickering()
    {
        isFlickering = false; // Stop the flickering process

        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine); // Stop the flickering coroutine
            flickerCoroutine = null;
        }

        windowRenderer.material = lightOnMaterial; // Set light to on material
    }

    // Method to check if the light is flickering
    public bool IsFlickering()
    {
        return isFlickering;
    }
}
