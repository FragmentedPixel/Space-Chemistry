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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.gameObject.GetComponent<PlayerInventory>();
        
        if(player != null && pickedUp == false)
        {
            pickedUp = true;
            Destroy(GetComponentInChildren<Transform>().gameObject);

            GetComponent<SpriteRenderer>().enabled = false;
            audioS.PlayOneShot(pickupSound);

            item.Collect();
            Destroy(gameObject, pickupSound.length);
        }
    }
}
