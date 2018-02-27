using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvetoryUI : UIMenu
{
    public InvetoryImage invetoryImagePrefab;
    
    public bool IsNeeded()
    {
        // If there are no collectabiles in this level, we don't need the pannel.
        CollectableObject[] objectsToCollect = FindObjectsOfType<CollectableObject>();
        return (objectsToCollect.Length <= 0);
    }

    public void HighLight()
    {
        anim.SetTrigger("Highlight-Trigger");
    }

    public void Hide()
    {
        anim.SetTrigger("Hide-Trigger");
    }

    public void OnItemsChanged()
    {
        // Destroy existing items.
        foreach (Transform oldItem in transform)
            Destroy(oldItem.gameObject);

        // Get current owned items.
        List<RepairItem> ownedItems = RepairItemsManager.GetAllOwnedItems();

        // Add the current owned items to the inventory.
        foreach(RepairItem item in ownedItems)
        {
            //TODO: Add object count as well.
            InvetoryImage newImage = Instantiate(invetoryImagePrefab, transform);
            newImage.SetUp(item.itemSprite, item.count);
        }
    }
}
