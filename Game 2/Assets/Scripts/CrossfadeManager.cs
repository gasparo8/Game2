using UnityEngine;
using UnityEngine.UI;

public class CrossfadeManager : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public Image crossfadeImage;

    void Start()
    {
        crossfadeImage.enabled = true;
        PlayCrossfadeIn();
    }

    public void PlayCrossfadeIn()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("FadeOut");
    }

    public void PlayCrossfadeOut()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("FadeIn");
    }

    public void DisableImage()
    {
        crossfadeImage.enabled = false; // Disable the image after the animation ends
    }
}
