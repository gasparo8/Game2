using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletWaterController : MonoBehaviour
{
    public Animator waterAnimator;  // Animator for toilet water animation
    private bool isPlaying = false;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is named "Toilet Handle"
                if (hit.collider.gameObject.name == "Toilet Handle")
                {
                    PlayWaterAnimation();
                }
            }
        }
    }

    public void PlayWaterAnimation()
    {
        if (waterAnimator != null && !isPlaying)
        {
            waterAnimator.Play("waterFlush", 0, 0.0f);
            isPlaying = true;
            StartCoroutine(AnimationCooldown());
        }
    }

    public IEnumerator AnimationCooldown()
    {
        // Wait for the animation duration, then allow it to play again
        yield return new WaitForSeconds(waterAnimator.GetCurrentAnimatorStateInfo(0).length);
        isPlaying = false;
    }
}
