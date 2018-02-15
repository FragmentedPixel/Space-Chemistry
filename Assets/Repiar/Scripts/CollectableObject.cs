using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Responsible for sending collected objects to the inventory.
 */

public class CollectableObject : SoundMonoBehaviour
{
    public RepairItem item;
    public AudioClip pickupSound;

    private bool pickedUp = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        
        if(player != null && pickedUp == false)
        {
            pickedUp = true;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();

            audioS.PlayOneShot(pickupSound);

            item.Collect();
            Destroy(gameObject, pickupSound.length);
        }
    }
}
