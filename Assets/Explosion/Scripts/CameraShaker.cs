using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float shakeDuration;
    public float delay;
    public AudioClip impactSound;

    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
        
        Invoke("ShakeCamera", delay);
    }

    private void ShakeCamera()
    {
        audioS.PlayOneShot(impactSound);
        FindObjectOfType<CameraShake>().Shake(shakeDuration);
    }

}
