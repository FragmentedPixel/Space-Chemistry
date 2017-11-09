using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's death and respawning.
 */

public class PlayerHealth : MonoBehaviour
{
    #region Parameters
    public float respawnDelay = 1f;

    private Vector3 currentCheckpoint;
    private bool dead = false;
    #endregion

    #region CheckPoint
    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        currentCheckpoint = newCheckPoint;
    }

    public void Death()
    {
        if(dead == false)
        {
            dead = true;
            StartCoroutine(RespawnPlayerCR(respawnDelay));
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
