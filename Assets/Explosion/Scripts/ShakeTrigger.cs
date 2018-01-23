using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : OneTimeEvent {

    public float shakeDuration;
    public AudioClip impactSound;

    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    public override void ActionHappening()
    {
        audioS.PlayOneShot(impactSound);
        FindObjectOfType<CameraShake>().Shake(shakeDuration);
    }
}
