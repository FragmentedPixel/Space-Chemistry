using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMonoBehaviour : MonoBehaviour
{
    public int soundPriority = 50;

    protected AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        if(audioS == null)
        {
            audioS = gameObject.AddComponent<AudioSource>();
        }

        audioS.priority = soundPriority;
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }


}
