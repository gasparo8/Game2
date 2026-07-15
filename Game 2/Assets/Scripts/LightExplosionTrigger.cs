using UnityEngine;

public class LightExplosionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;
    [SerializeField] private AudioSource explosionSound;

    private int triggerCount = 0;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggerCount++;

        if (triggerCount == 2)
        {
            triggered = true;

            if (lightObject != null)
                lightObject.SetActive(false);

            if (explosionSound != null)
                explosionSound.Play();
        }
    }
}