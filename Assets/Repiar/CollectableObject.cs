using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public InventoryItem item;
    public AudioClip pickupSound;

    private bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.gameObject.GetComponent<PlayerInventory>();
        
        if(player != null && pickedUp == false)
        {
            pickedUp = true;


            GetComponent<SpriteRenderer>().enabled = false;
            AudioSource audioS = gameObject.AddComponent<AudioSource>();
            audioS.volume = PlayerPrefsManager.GetMasterVolume();
            audioS.PlayOneShot(pickupSound);
            Destroy(gameObject.GetComponentInChildren<Transform>().gameObject);

            player.AddItem(item);
            Destroy(gameObject, pickupSound.length);
        }
    }


}
