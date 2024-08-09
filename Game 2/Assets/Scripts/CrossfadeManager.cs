using UnityEngine;
using UnityEngine.UI;

public class CrossfadeManager : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public Image crossfadeImage;

    void Start()
    {
        crossfadeImage.enabled = false; // Make sure the image is disabled initially
        PlayCrossfadeIn();
    }

    public void PlayCrossfadeIn()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("Fade_In");
    }

    public void PlayCrossfadeOut()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("Fade_Out");
    }

    public void DisableImage()
    {
        crossfadeImage.enabled = false; // Disable the image after the animation ends
    }
}
