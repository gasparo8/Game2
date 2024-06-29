using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;

    private bool doorOpen = false;
    private bool animationCooldown = false;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!animationCooldown)
        {
            if (!doorOpen)
            {
                doorAnim.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
            }
            else
            {
                doorAnim.Play("DoorClose", 0, 0.0f);
                doorOpen = false;
            }

            // Start the cooldown timer
            StartCoroutine(AnimationCooldownTimer());
        }
    }

    private IEnumerator AnimationCooldownTimer()
    {
        // Wait for 1 second before allowing the animation again
        animationCooldown = true;
        yield return new WaitForSeconds(1.0f);
        animationCooldown = false;
    }
}