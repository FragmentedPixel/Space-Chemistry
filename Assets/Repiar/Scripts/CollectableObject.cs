using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Responsible for sending collected objects to the inventory.
 */

public class CollectableObject : OneTimeSoundTrigger
{
    public RepairItem item;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    protected override void OnPlayerTriggered()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Stop();

        item.Collect();
        Destroy(gameObject, triggerSound.length);
    }

   
}
