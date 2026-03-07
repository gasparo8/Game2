using UnityEngine;

public class PhoneController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // Phone stays enabled, no need to disable renderers
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayPhoneAnimation();
        }
    }

    public void PlayPhoneAnimation()
    {
        if (animator != null)
            animator.SetTrigger("PhoneRaise"); // Trigger your animation
    }
}