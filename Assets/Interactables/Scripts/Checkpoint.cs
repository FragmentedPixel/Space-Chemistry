﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for updating the player's checkpoint when triggered by him.
 */

public class Checkpoint : SoundMonoBehaviour
{
    // checked sprite.
    public Sprite checkpointPassed;

    // Sound played when the player passes the checkpoint.
    public AudioClip passedSound;

    //Was this checkpoint already passed?
    private bool passed = false;

    // Renderer component.
    private SpriteRenderer spriterenderer;

    // Light
    public Light light;

    void Start()
    {
        spriterenderer = GetComponentInChildren<SpriteRenderer>();
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null && passed == false)
        {
            passed = true;
            spriterenderer.sprite = checkpointPassed;

            light.color = Color.green;
            audioS.PlayOneShot(passedSound);
            player.SetCheckPoint(new Vector3(transform.position.x,transform.position.y,-1));
        }
    }
}
