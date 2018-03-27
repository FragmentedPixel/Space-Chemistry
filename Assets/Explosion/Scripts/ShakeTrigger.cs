using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : OneTimeSoundTrigger {

    public float shakeDuration;

    protected override void OnPlayerTriggered()
    {
        FindObjectOfType<CameraShake>().Shake(shakeDuration);
    }
}
