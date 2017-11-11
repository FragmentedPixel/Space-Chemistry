using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for killing the player.
 */ 

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.Death();
        }
    }
}
