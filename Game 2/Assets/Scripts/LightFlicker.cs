using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public float minFlickerTime = 0.1f; // Minimum flicker time
    public float maxFlickerTime = 0.5f; // Maximum flicker time
    private Renderer windowRenderer;

    private void Start()
    {
        // Automatically gets the Renderer component from the GameObject this script is attached to
        windowRenderer = GetComponent<Renderer>();

        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
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
