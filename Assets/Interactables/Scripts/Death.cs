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
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            PlayerHealth health = player.GetComponentInChildren<PlayerHealth>();
            health.Death();
        }
    }
}
