using UnityEngine;

public class LockHighlight : MonoBehaviour
{
    private Renderer lockRenderer;
    private Material lockMaterial;
    private Color originalColor;
    private bool isHighlighted = false;

    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float pulseSpeed = 2f; // Speed of pulse
    [SerializeField] private float pulseIntensity = 0.5f; // How strong the pulse gets

    private void Awake()
    {
        lockRenderer = GetComponent<Renderer>();
        lockMaterial = lockRenderer.material;
        originalColor = lockMaterial.color;
    }

    private void Update()
    {
        if(TaskManager.ShowLockHighlights)
        {
            float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;
            Color pulsingColor = Color.Lerp(originalColor, highlightColor, pulseIntensity * pulse);
            lockMaterial.color = pulsingColor;
        }
    }

    public void HighlightLock(bool highlight)
    {
        isHighlighted = highlight;

        if (!highlight)
        {
            lockMaterial.color = originalColor;
        }
    }
}
