using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's death and respawning.
 */

public class PlayerHealth : MonoBehaviour
{
    #region Parameters
    public AudioClip deathSound;

    private AudioSource audioS;
    private Vector3 currentCheckpoint;
    private bool dead = false;
    #endregion

    #region CheckPoint
    private void Start()
    {
        audioS = gameObject.AddComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        currentCheckpoint = newCheckPoint;
        Debug.Log(currentCheckpoint);
    }

    public void Death()
    {
        if(dead == false)
        {
            audioS.PlayOneShot(deathSound);
            dead = true;
            StartCoroutine(RespawnPlayerCR(deathSound.length));
        }
    }

    private void RespawnPlayer()
    {
        transform.position = currentCheckpoint;
        dead = false;
    }

    IEnumerator RespawnPlayerCR(float time)
    {
        yield return new WaitForSeconds(time);
        RespawnPlayer();

    }
    #endregion
}
