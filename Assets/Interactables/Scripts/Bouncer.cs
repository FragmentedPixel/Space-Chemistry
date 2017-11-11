using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for bouncing the player back when colliding with him.
 */

public class Bouncer : MonoBehaviour
{
    public Vector3 bounceForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            // TODO: Bounce the player in the diretion he came from.
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().AddForce(bounceForce);
        }
    }
}
