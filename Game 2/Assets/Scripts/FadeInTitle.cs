using UnityEngine;
using TMPro;

public class FadeInTitle : MonoBehaviour
{
    [Header("References")]
    public TMP_Text titleText;             // Assign your TextMeshPro title here

    [Header("Timing")]
    public float fadeDuration = 2f;        // Duration of fade (seconds)
    public float delayBeforeFade = 0.5f;   // Optional delay before fade begins

    [Header("Motion")]
    public float moveDistance = 20f;       // Pixels/units to move upward during fade
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Smooth easing

    private void Start()
    {
        if (titleText != null)
            StartCoroutine(FadeAndMoveIn());
    }

    private System.Collections.IEnumerator FadeAndMoveIn()
    {
        // Start transparent and slightly lower
        Color color = titleText.color;
        color.a = 0f;
        titleText.color = color;

        Vector3 startPos = titleText.transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);

        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float easedT = fadeCurve.Evaluate(t);

            // Fade alpha
            color.a = easedT;
            titleText.color = color;

            // Move upward
            titleText.transform.localPosition = Vector3.Lerp(startPos, endPos, easedT);

            yield return null;
        }

        // Ensure final state
        color.a = 1f;
        titleText.color = color;
        titleText.transform.localPosition = endPos;
    }
}
