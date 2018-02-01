using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image original;

    private List<Image> images = new List<Image>();
    private PlayerInventory playerInvetory;

    private void Start()
    {
        CollectableObject[] objectsToCollect = FindObjectsOfType<CollectableObject>();
        if(objectsToCollect.Length <= 0)
        {
            gameObject.SetActive(false);
        }
    }

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
                Image imageToDelete = im;
                images.Remove(im);

                Destroy(imageToDelete.gameObject);

                return;
            }
        }
    }

}
