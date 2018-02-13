using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMonoBehaviour : MonoBehaviour
{
    public int priority;

    protected AudioSource audioS;

    private void Start()
    {
        Debug.Log("da");

        audioS = GetComponent<AudioSource>();
        if(audioS == null)
        {
            audioS = gameObject.AddComponent<AudioSource>();
        }

        audioS.priority = priority;
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }


}
