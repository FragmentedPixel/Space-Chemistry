using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public Vector3 bounceForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().AddForce(bounceForce);
        }
    }
}
