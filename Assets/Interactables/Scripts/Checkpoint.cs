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

    // Sound played when the player passes the checkpoint.
    public AudioClip passedSound;

    //Was this checkpoint already passed?
    private bool passed = false;

    // Renderer component.
    private SpriteRenderer spriterenderer;

    // AudioS component.
    private AudioSource audioS;

    void Start()
    {
        spriterenderer = GetComponentInChildren<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();

        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null && passed == false)
        {
            passed = true;
            spriterenderer.sprite = checkpointPassed;
            audioS.PlayOneShot(passedSound);
            player.SetCheckPoint(transform.position);
        }
    }
}
