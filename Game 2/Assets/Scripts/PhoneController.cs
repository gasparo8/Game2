using UnityEngine;

public class PhoneController : MonoBehaviour
{
    private Animator animator;

    public float idleTimeBeforePhone = 6f; // Seconds of no input before phone animation

    private float idleTimer = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Manual trigger
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayPhoneAnimation();
        }

        // Check for any movement/input keys
        bool hasInput =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f ||
            Input.anyKeyDown;

        if (hasInput)
        {
            // Reset timer when player gives input
            idleTimer = 0f;
        }
        else
        {
            // Count idle time
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeBeforePhone)
            {
                PlayPhoneAnimation();
                idleTimer = 0f; // Reset so it doesn't spam constantly
            }
        }
    }

    public void PlayPhoneAnimation()
    {
        if (animator != null)
            animator.SetTrigger("PhoneRaise");
    }
}