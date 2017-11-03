using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform checkPoint;
    public bool dead = false;

    public Vector3 currentCheckpoint;

    public void Death()
    {
        if(dead == false)
        {
            dead = true;
            StartCoroutine(ExecuteAfterTime(1));
        }
    }

    #region CheckPoint
    IEnumerator ExecuteAfterTime(float time)
    {

        yield return new WaitForSeconds(time);
        RespawnPlayer();

    }

    public void RespawnPlayer()
    {
        transform.position = currentCheckpoint;
        dead = false;
    }
    #endregion
}
