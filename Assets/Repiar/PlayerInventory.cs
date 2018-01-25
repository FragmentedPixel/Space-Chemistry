using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> inventory;
    public RepiarableItem repairedItem;

	public void AddItem(InventoryItem newItem)
    {
        inventory.Add(newItem);
        FindObjectOfType<InventoryManager>().AddImage(newItem.image);
    }

    public bool HasItem(InventoryItem itemRequested, int amount)
    {
        int counter = 0;
        foreach(InventoryItem item in inventory)
        {
            if (item == itemRequested)
                counter++;
        }

        bool result = (amount <= counter);


        if(result == true)
        {
            int currentAmount = 0;

            for(int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i] == itemRequested)
                {

                }
            }
        }

        return result;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            FindObjectToRepair();
        }

        if(Input.GetKeyUp(KeyCode.Q))
        {
            StopRepairingObject();
        }
    }

    private void FindObjectToRepair()
    {
        float repiarRange = 3f;
        RepiarableItem[] items = FindObjectsOfType<RepiarableItem>();
        foreach(RepiarableItem item in items)
        {
            float distance = Vector2.Distance(item.transform.position, transform.position);
            if(distance < repiarRange)
            {
                repairedItem = item;
                break;
            }
        }

        repairedItem.StartRepair();
    }

    private void StopRepairingObject()
    {
        if (repairedItem != null)
        {
            repairedItem.StopRepair();
        }
    }
}
