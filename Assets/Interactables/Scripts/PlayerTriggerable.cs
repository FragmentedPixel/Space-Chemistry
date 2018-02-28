using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerable : MonoBehaviour
{
    private bool triggered = false;
    public AudioClip triggerSound;

    public int soundPriority = 50;
    protected AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        if (audioS == null)
        {
            audioS = gameObject.AddComponent<AudioSource>();
        }

        audioS.priority = soundPriority;
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

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
