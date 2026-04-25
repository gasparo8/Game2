using UnityEngine;

public class PhoneController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Manual trigger only
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayPhoneAnimation();
        }
    }

    public void PlayPhoneAnimation()
    {
        if (animator != null)
            animator.SetTrigger("PhoneRaise");
    }
}