using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTitle : MonoBehaviour
{
    [Header("References")]
    public TMP_Text titleText;             // Assign your TextMeshPro title here

    [Header("Timing")]
    public float fadeDuration = 2f;        // Duration of fade (seconds)
    public float delayBeforeFade = 0.5f;   // Optional delay before fade begins

    [Header("Motion")]
    public float moveDistance = 20f;       // Units to move downward into place
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Smooth easing

    private void Start()
    {
        if (titleText != null)
            StartCoroutine(FadeAndMoveIn());
    }

    private IEnumerator FadeAndMoveIn()
    {
        // Start transparent
        Color color = titleText.color;
        color.a = 0f;
        titleText.color = color;

        // FINAL position (where it should end)
        Vector3 endPos = titleText.transform.localPosition;

        // START ABOVE the final position
        Vector3 startPos = endPos + new Vector3(0, moveDistance, 0);

        // Force starting position
        titleText.transform.localPosition = startPos;

        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float easedT = fadeCurve.Evaluate(t);

            // Fade in
            color.a = easedT;
            titleText.color = color;

            // Move DOWN into final position
            titleText.transform.localPosition = Vector3.Lerp(startPos, endPos, easedT);

            yield return null;
        }

        // Ensure final state
        color.a = 1f;
        titleText.color = color;
        titleText.transform.localPosition = endPos;
    }
}