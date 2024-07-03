using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform cameraTransform; // Reference to the camera transform
    public float bobbingSpeed = 14f; // Speed of the bobbing effect
    public float bobbingAmount = 0.05f; // Amount of bobbing effect

    Vector3 velocity;
    bool isGrounded;
    float defaultYPos = 0;
    float timer = 0;

    void Start()
    {
        if (cameraTransform != null)
        {
            defaultYPos = cameraTransform.localPosition.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (cameraTransform != null)
        {
            ApplyHeadBobbingEffect(x, z);
        }
    }

    void ApplyHeadBobbingEffect(float x, float z)
    {
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            // Player is moving
            timer += Time.deltaTime * bobbingSpeed;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, defaultYPos + Mathf.Sin(timer) * bobbingAmount, cameraTransform.localPosition.z);
        }
        else
        {
            // Player is stationary
            timer = 0;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, Mathf.Lerp(cameraTransform.localPosition.y, defaultYPos, Time.deltaTime * bobbingSpeed), cameraTransform.localPosition.z);
        }
    }
}
