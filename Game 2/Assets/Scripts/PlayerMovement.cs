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

    public Transform cameraTransform;
    public float bobbingSpeed = 14f;
    public float verticalBobbingAmount = 0.05f;
    public float horizontalBobbingAmount = 0.05f;

    Vector3 velocity;
    bool isGrounded;
    float defaultYPos = 0;
    float defaultXPos = 0;
    float timer = 0;

    // Audio-related variables
    public AudioSource audioSource;
    public AudioClip[] woodStepSounds;
    public AudioClip[] grassStepSounds;
    public AudioClip nightAmbience;
    public float stepInterval = 0.5f; // Time between steps
    public float fadeOutDuration = 2f; // Duration of the fade-out effect
    private float stepTimer = 0f;

    // Index trackers for step sounds
    private int woodStepIndex = 0;
    private int grassStepIndex = 0;

    private Coroutine fadeOutCoroutine = null; // Reference to the fade-out coroutine
    private bool isOnGrass = false; // Tracks if the player is currently on grass

    void Start()
    {
        if (cameraTransform != null)
        {
            defaultYPos = cameraTransform.localPosition.y;
            defaultXPos = cameraTransform.localPosition.x;
        }
    }

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

        if (isGrounded && (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f))
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
    }

    void ApplyHeadBobbingEffect(float x, float z)
    {
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            // Player is moving
            timer += Time.deltaTime * bobbingSpeed;
            float waveSlice = Mathf.Sin(timer);
            float horizontalWaveSlice = Mathf.Sin(timer * 0.5f); // slower horizontal bobbing

            float verticalBob = waveSlice * verticalBobbingAmount;
            float horizontalBob = horizontalWaveSlice * horizontalBobbingAmount;

            cameraTransform.localPosition = new Vector3(defaultXPos + horizontalBob, defaultYPos + verticalBob, cameraTransform.localPosition.z);
        }
        else
        {
            // Player is stationary
            timer = 0;
            cameraTransform.localPosition = new Vector3(Mathf.Lerp(cameraTransform.localPosition.x, defaultXPos, Time.deltaTime * bobbingSpeed), Mathf.Lerp(cameraTransform.localPosition.y, defaultYPos, Time.deltaTime * bobbingSpeed), cameraTransform.localPosition.z);
        }
    }

    void PlayStepSound()
    {
        RaycastHit hit;
        Vector3 raycastDirection = Vector3.down;
        float raycastDistance = groundDistance + 0.1f;

        if (Physics.Raycast(groundCheck.position, raycastDirection, out hit, raycastDistance))
        {
            // Check the tag of the surface the player is standing on
            if (hit.collider.CompareTag("WoodFloor"))
            {
                PlayNextClip(woodStepSounds, ref woodStepIndex);
                if (isOnGrass)
                {
                    StopNightAmbience();
                    isOnGrass = false;
                }
            }
            else if (hit.collider.CompareTag("Grass"))
            {
                PlayNextClip(grassStepSounds, ref grassStepIndex);
                if (!isOnGrass)
                {
                    PlayNightAmbience();
                    isOnGrass = true;
                }
            }
        }
    }

    void PlayNightAmbience()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }

        // Ensure the audio source is set to play nightAmbience and then start the sound
        audioSource.clip = nightAmbience;
        audioSource.loop = true;
        audioSource.volume = 1f; // Reset volume to full before playing
        audioSource.Play();
    }

    void StopNightAmbience()
    {
        if (fadeOutCoroutine == null)
        {
            fadeOutCoroutine = StartCoroutine(FadeOutNightAmbience());
        }
    }

    IEnumerator FadeOutNightAmbience()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    void PlayNextClip(AudioClip[] clips, ref int clipIndex)
    {
        if (clips.Length > 0)
        {
            AudioClip clip = clips[clipIndex];
            audioSource.PlayOneShot(clip);

            // Move to the next clip, looping back to the start if at the end
            clipIndex = (clipIndex + 1) % clips.Length;
        }
    }
}
