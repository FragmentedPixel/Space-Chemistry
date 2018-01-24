using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image original;

    private List<Image> images = new List<Image>();
    private PlayerInventory playerInvetory;

    public void AddImage(Sprite newSprite)
    {
        Image newImage = Instantiate(original, transform);
        newImage.sprite = newSprite;

        images.Add(newImage);
    }

    public void RemoveImage(Sprite spriteToRemove)
    {
        foreach(Image im in images)
        {
            if(im.sprite == spriteToRemove)
            {
                images.Remove(im);
                return;
            }
        }
    }

}
