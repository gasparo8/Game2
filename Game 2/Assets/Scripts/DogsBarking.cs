using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DogsBarking : MonoBehaviour
{
    public AudioSource dogsBarkingAudio;
    public GameObject dogsBarkingCutscene;
    public float delayUntilBarking = 10f;

    public void StartBarking()
    {
        StartCoroutine(PlayDogsBarking());
    }

    private IEnumerator PlayDogsBarking()
    {
        if (dogsBarkingAudio != null)
        {
            dogsBarkingAudio.Play();
        }

        yield return new WaitForSeconds(delayUntilBarking);

        if (dogsBarkingCutscene != null)
        {
            dogsBarkingCutscene.SetActive(true);
        }
    }
}
