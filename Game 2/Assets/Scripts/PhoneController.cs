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

        bool hasInput =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f ||
            Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
            Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f ||
            Input.anyKeyDown ||
            Input.GetMouseButton(0) ||
            Input.GetMouseButton(1);

        if (hasInput)
        {
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeBeforePhone)
            {
                PlayPhoneAnimation();
                idleTimer = 0f;
            }
        }
    }

    public void PlayPhoneAnimation()
    {
        if (animator != null)
            animator.SetTrigger("PhoneRaise");
    }
}