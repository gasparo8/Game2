using UnityEngine;

public class WalkingObjectController : MonoBehaviour
{
    public Renderer targetRenderer;

    public void ShowObject()
    {
        if (targetRenderer != null)
        {
            targetRenderer.enabled = true;
        }
    }

    public void HideObject()
    {
        if (targetRenderer != null)
        {
            targetRenderer.enabled = false;
        }
    }
}