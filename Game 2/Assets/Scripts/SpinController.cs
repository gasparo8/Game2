using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    private Animator TP;

    private bool tpSpin = false;
    private bool animationCooldown = false;

    private void Awake()
    {
        TP = gameObject.GetComponent<Animator>();
    }
    
    public void PlayTPAnimation()
    {
        if (!animationCooldown)
        {
            if (!tpSpin)
            {
                TP.Play("tpSpin", 0, 0.0f);
                tpSpin = true;
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
        tpSpin = false;
    }
}
