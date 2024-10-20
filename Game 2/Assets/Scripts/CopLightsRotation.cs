using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopLightsBlink : MonoBehaviour
{
    public Light redLight;   // Assign the red light (Light component) in the Inspector
    public Light blueLight;  // Assign the blue light (Light component) in the Inspector
    public float blinkDuration = 0.2f;  // Time for each blink
    public float delayBetweenColors = 0.5f;  // Time between switching from red to blue

    void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(BlinkLights());
    }

    IEnumerator BlinkLights()
    {
        while (true)
        {
            // Blink the red light twice
            yield return StartCoroutine(BlinkTwice(redLight));
            // Small delay between red and blue blinking
            yield return new WaitForSeconds(delayBetweenColors);

            // Blink the blue light twice
            yield return StartCoroutine(BlinkTwice(blueLight));
            // Small delay between blue and red blinking
            yield return new WaitForSeconds(delayBetweenColors);
        }
    }

    // Coroutine to handle blinking twice for a given light
    IEnumerator BlinkTwice(Light light)
    {
        for (int i = 0; i < 2; i++)
        {
            light.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
            light.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
