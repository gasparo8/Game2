using UnityEngine;

public class FaucetWaterEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterParticleSystem;

    public void EmitWater()
    {
        if (waterParticleSystem != null)
        {
            waterParticleSystem.Play();
            Invoke(nameof(StopWater), 5f);
        }
    }

    private void StopWater()
    {
        if (waterParticleSystem != null)
        {
            waterParticleSystem.Stop();
        }
    }
}
