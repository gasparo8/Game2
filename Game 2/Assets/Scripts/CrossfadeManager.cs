using UnityEngine;
using UnityEngine.UI;

public class CrossfadeManager : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public Image crossfadeImage;

    public void PlayCrossfadeInOut()
    {
        crossfadeAnimator.SetTrigger("FadeInOut");
    }
}
