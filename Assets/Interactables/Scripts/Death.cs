using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
