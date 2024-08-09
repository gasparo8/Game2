/*(using UnityEngine;
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
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrossfadeManager : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public Image crossfadeImage;

    public IEnumerator PlayCrossfadeInOut()
    {
        crossfadeImage.enabled = true;
        crossfadeAnimator.SetTrigger("FadeInOut");

        // Wait until the FadeInOut animation is finished
        yield return new WaitForSeconds(crossfadeAnimator.GetCurrentAnimatorStateInfo(1).length);
        crossfadeImage.enabled = false;
    }
}
