using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for bouncing the player back when colliding with him.
 */

public class Bouncer : MonoBehaviour
{
    public float bounceForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.velocity = - playerRb.velocity.normalized * bounceForce;
        }
    }
}
