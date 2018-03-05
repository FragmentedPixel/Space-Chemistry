using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for killing the player.
 */ 

public class Death : PlayerTriggerable
{
    protected override void OnPlayerTriggered()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();

        if (player != null)
        {
            PlayerHealth health = player.GetComponentInChildren<PlayerHealth>();
            health.Death();
        }
    }
}
