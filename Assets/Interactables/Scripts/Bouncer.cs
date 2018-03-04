using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for bouncing the player back when colliding with him.
 */

public class Bouncer : MonoBehaviour
{
    public float bouncerForce = 25f;
    public AudioClip hitSound;

    private AudioSource audioS;

    private void Start()
    {
        audioS = gameObject.AddComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player)
        {
            audioS.PlayOneShot(hitSound);

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.AddForce(-playerRb.velocity.normalized * bouncerForce, ForceMode2D.Impulse);
        }
    }
}
