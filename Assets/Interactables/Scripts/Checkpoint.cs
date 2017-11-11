using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for updating the player's checkpoint when triggered by him.
 */

public class Checkpoint : MonoBehaviour
{
    // checked sprite.
    public Sprite checkpointPassed;

    // Renderer component.
    private SpriteRenderer spriterenderer;

    void Start()
    {
        spriterenderer = GetComponentInChildren<SpriteRenderer>();
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            spriterenderer.sprite = checkpointPassed;
            player.SetCheckPoint(transform.position);
        }
    }
}
