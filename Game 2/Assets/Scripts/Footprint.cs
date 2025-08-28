using System.Collections;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private bool isCleaning = false;

    [Header("Audio")]
    [SerializeField] private AudioClip cleanSound; // Assign in Inspector
    private AudioSource audioSource;

    [Header("Mop Motion")]
    [SerializeField] private float mopMoveDistance = 0.15f; // How far mop moves
    [SerializeField] private float mopMoveSpeed = 8f;       // How fast it oscillates

    private FootprintManager footprintManager;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Find the FootprintManager in the scene
        footprintManager = FindObjectOfType<FootprintManager>();

        // Try to get AudioSource, add one if missing
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    // Now requires mopTransform so we can animate it
    public void CleanFootprint(System.Action onFinished, Transform mopTransform)
    {
        if (!isCleaning)
            StartCoroutine(FadeAndDestroy(onFinished, mopTransform));
    }

    private IEnumerator FadeAndDestroy(System.Action onFinished, Transform mopTransform)
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

        // Remember mop’s starting local position
        Vector3 startPos = mopTransform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Fade footprint
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(color.r, color.g, color.b, alpha);

            // Animate mop forward/back using sine wave
            float wave = Mathf.Sin(elapsed * mopMoveSpeed);
            mopTransform.localPosition = startPos + mopTransform.up * wave * mopMoveDistance;

            yield return null;
        }

        // Reset mop back to its original position
        mopTransform.localPosition = startPos;

        // Notify manager this footprint is done
        if (footprintManager != null)
        {
            footprintManager.FootprintCleaned();
        }

        //Destroy(gameObject);
        onFinished?.Invoke(); // Notify that this footprint is gone
    }
}
