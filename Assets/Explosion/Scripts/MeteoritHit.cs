using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritHit : OneTimeEvent
{
    public AudioClip meteoritHitSound;
    public GameObject containerPrefab;
    public Transform containerSpawn;

    private AudioSource audioS;

    public override void ActionHappening()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
        audioS.PlayOneShot(meteoritHitSound);

        Instantiate(containerPrefab, containerSpawn.position, containerPrefab.transform.rotation, containerSpawn);
    }


}
