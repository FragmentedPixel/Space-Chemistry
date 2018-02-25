using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for unlocking the barrels through the level.
 */

public class ProgressiveBarrel : MonoBehaviour
{
    public AudioClip unlockSound;

    private ContainersUI containersManager;
    private AudioSource audioS;
    private bool used = false;

    private void Start()
    {
        containersManager = FindObjectOfType<ContainersUI>();
        containersManager.LockAllBarrels();

        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if(player != null && used == false)
        {
            used = true;
            containersManager.UnLockNextBarrel();
            audioS.PlayOneShot(unlockSound);
        }
    }

    private void Update()
    {
        if(used)
        {
            foreach (Transform t in transform)
                Destroy(t.gameObject);
        }
    }
}
