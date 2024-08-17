/*using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public float minFlickerTime = 0.1f; // Minimum flicker time
    public float maxFlickerTime = 0.5f; // Maximum flicker time
    public Renderer windowRenderer;

    private void Start()
    {
        // Automatically gets the Renderer component from the GameObject this script is attached to
        windowRenderer = GetComponent<Renderer>();

        StartCoroutine(FlickerLight());
    }

    public IEnumerator FlickerLight()
    {
        while (true)
        {
            float flickerTime = Random.Range(minFlickerTime, maxFlickerTime);

            // Toggle material
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
}
*/
using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public float minFlickerTime = 0.1f; // Minimum flicker time
    public float maxFlickerTime = 0.5f; // Maximum flicker time
    public Renderer windowRenderer;

    private bool isFlickering = false;

    private void Start()
    {
        windowRenderer = GetComponent<Renderer>();
        StartCoroutine(FlickerLight());
    }

    public IEnumerator FlickerLight()
    {
        isFlickering = true; // Flickering has started

        while (true)
        {
            float flickerTime = Random.Range(minFlickerTime, maxFlickerTime);

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

    public bool IsFlickering()
    {
        return isFlickering;
    }

    // Optionally, you can stop flickering by adding this method
    public void StopFlickering()
    {
        isFlickering = false;
        StopCoroutine(FlickerLight());
    }
}
