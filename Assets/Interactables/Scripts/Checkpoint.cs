using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Sprite checkpointPassed;

    private SpriteRenderer spriterenderer;

    void Start()
    {
        spriterenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
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
