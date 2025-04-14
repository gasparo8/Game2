using UnityEngine;

public class FaucetWaterEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterParticleSystem1;
    [SerializeField] private ParticleSystem waterParticleSystem2;

    public void EmitWater()
    {
        if (waterParticleSystem1 != null) waterParticleSystem1.Play();
        if (waterParticleSystem2 != null) waterParticleSystem2.Play();

        Invoke(nameof(StopWater), 5f);
    }

    private void StopWater()
    {
        if (waterParticleSystem1 != null) waterParticleSystem1.Stop();
        if (waterParticleSystem2 != null) waterParticleSystem2.Stop();
    }
}
