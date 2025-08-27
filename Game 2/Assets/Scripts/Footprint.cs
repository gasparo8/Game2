using System.Collections;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private bool isCleaning = false;

    [Header("Audio")]
    [SerializeField] private AudioClip cleanSound; // Assign in Inspector
    private AudioSource audioSource;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Try to get AudioSource, add one if missing
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void CleanFootprint(System.Action onFinished)
    {
        if (!isCleaning)
            StartCoroutine(FadeAndDestroy(onFinished));
    }

    private IEnumerator FadeAndDestroy(System.Action onFinished)
    {
        isCleaning = true;

        // Play cleaning sound once
        if (cleanSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(cleanSound);
        }

        Material mat = meshRenderer.material;
        Color color = mat.color;

        float duration = 3.5f; // seconds to fade
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
        onFinished?.Invoke(); // Notify that this footprint is gone
    }
}
