using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeSoundTrigger : SoundMonoBehaviour
{
    private bool triggered = false;
    public AudioClip triggerSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if(player != null && triggered == false)
        {
            if (triggerSound != null)
                audioS.PlayOneShot(triggerSound);

            triggered = true;
            OnPlayerTriggered();
        }
    }

    protected virtual void OnPlayerTriggered()
    {
        Debug.Log("Player Entered");
    }
}
