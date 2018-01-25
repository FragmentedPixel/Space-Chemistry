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

    public void RemoveItems(InventoryItem itemRequested, int amount)
    {
        int currentAmount = 0;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemRequested)
            {
                FindObjectOfType<InventoryManager>().RemoveImage(itemRequested.image);
                currentAmount++;
                inventory.RemoveAt(i);
                i--;
            }

            if (currentAmount == amount)
            {
                return;
            }
        }
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

        return result;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Repiar"))
        {
            FindObjectToRepair();
        }

        if(Input.GetButtonUp("Repiar"))
        {
            StopRepairingObject();
        }
    }

    private void FindObjectToRepair()
    {
        float repiarRange = 3f;
        repairedItem = null;
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

        if(repairedItem != null)
        {
            repairedItem.StartRepair();
        }
        else
        {
            MessageManager.getInstance().DissplayMessage("There is nothing to repair.", 3f);
        }

    }

    private void StopRepairingObject()
    {
        if (repairedItem != null)
        {
            repairedItem.StopRepair();
        }
    }
}
