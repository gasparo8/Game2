using UnityEngine;
using UnityEngine.UI;

public class CrossfadeManager : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public Image crossfadeImage;

    public void PlayCrossfadeInOut()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("FadeInOut");
    }
}
