using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RepairItemsManager
{
    public static List<RepairItem> allItems = new List<RepairItem>();

    public static void Update()
    {
        Object.FindObjectOfType<InvetoryPanel>().OnItemsChanged();
    }

    public static void AddItem(RepairItem newItem)
    {
        allItems.Add(newItem);
    }

    public static List<RepairItem> GetAllOwnedItems()
    {
        List<RepairItem> ownedItems = new List<RepairItem>();

        foreach(RepairItem item in allItems)
        {
            if (item.Has(1))
                ownedItems.Add(item);
        }

        return ownedItems;
    }

    public static List<RepairItem> GetAllItems()
    {
        return allItems;
    }
}
