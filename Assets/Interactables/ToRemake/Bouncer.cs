 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for bouncing the player back when colliding with him.
 */

public class Bouncer : OneTimeSoundTrigger
{
    public float bouncerForce = 25f;

    protected override void OnPlayerTriggered()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce(-playerRb.velocity.normalized * bouncerForce, ForceMode2D.Impulse);
    }
}
