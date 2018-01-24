using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public InventoryItem item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.gameObject.GetComponent<PlayerInventory>();
        
        if(player != null)
        {
            player.AddItem(item);
            Destroy(gameObject);
        }
    }


}
