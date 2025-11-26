using UnityEngine;

public class ChangeTagOnAnim : MonoBehaviour
{
    public string newTag;

    public void SetTag()
    {
        gameObject.tag = newTag;
    }
}
