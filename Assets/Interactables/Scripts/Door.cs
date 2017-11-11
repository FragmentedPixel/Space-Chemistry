using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for transferring the player to the next level when reached.
 */

public class Door : MonoBehaviour
{
    public string NextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;

        if(player.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().ChangeScene(NextLevel);
        }
    }
}
