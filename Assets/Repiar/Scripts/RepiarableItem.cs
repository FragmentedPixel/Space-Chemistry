using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepiarableItem : MonoBehaviour
{
    public float repiarDuration;
    public Image repiarProgress;
    public ParticleSystem repairParticles;

    public List<ItemNeeded> itemsNeeded;

    public Canvas repiarCanvas;
    public Canvas overviewCanvas;

    public Image original;
    public Transform itemsNeedPannel;
    public Text repiarText;

    private void Start()
    {
        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            ItemNeeded currentItem = itemsNeeded[i];
            for(int j = 0; j < currentItem.amount; j++)
            {
                Image newImage = Instantiate(original, itemsNeedPannel);
                newImage.sprite = currentItem.item.itemSprite;
            }   
        }
    }

    public void StartRepair()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        repiarText.enabled = false;
        repairParticles.Play();

        if (HasAllItems())
        {
            StartCoroutine(RepairingCR());
        }
        else
        {
            MessageManager.getInstance().DissplayMessage("Not enough items in inventory.", 3f);
        }
    }

    private void UseAllItems()
    {
        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            ItemNeeded currentItem = itemsNeeded[i];
            for (int j = 0; j < currentItem.amount; j++)
            {
                currentItem.item.Use(currentItem.amount);
            }
        }
    }

    private bool HasAllItems()
    {
        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            ItemNeeded currentItem = itemsNeeded[i];
            for (int j = 0; j < currentItem.amount; j++)
            {
                if (currentItem.item.Has(currentItem.amount) == false)
                    return false;
            }
        }

        return true;
    }

    public void StopRepair()
    {
        repiarText.enabled = true;
        repairParticles.Stop();
        repairParticles.Clear();
        
        repiarProgress.fillAmount = 0f;
        StopAllCoroutines();
    }

    private IEnumerator RepairingCR()
    {

        float currentTime = 0f;
        while(currentTime <= repiarDuration)
        {
            repiarProgress.fillAmount = currentTime / repiarDuration;
            currentTime += Time.deltaTime;

            yield return null;
        }

        RepairedItem();

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            repiarCanvas.enabled = true;
            overviewCanvas.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            repiarCanvas.enabled = false;
            overviewCanvas.enabled = true;
        }
    }

    public virtual void RepairedItem()
    {
        PlayerInventory player = FindObjectOfType<PlayerInventory>();

        UseAllItems();

        FindObjectOfType<StartConnecting>().StartFilling();

        overviewCanvas.gameObject.SetActive(false);
        repiarCanvas.gameObject.SetActive(false);
        repairParticles.Stop();

        Destroy(this);
    }
}

[System.Serializable]
public class ItemNeeded
{
    public RepairItem item;
    public int amount;
}