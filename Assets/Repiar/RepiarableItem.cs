using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepiarableItem : MonoBehaviour
{
    public float repiarDuration;
    public Image repiarProgress;
    public ParticleSystem repairParticles;

    public InventoryItem itemNeeded;
    public int amount;

    public Canvas repiarCanvas;
    public Canvas overviewCanvas;

    public Image original;
    public Transform itemsNeedPannel;
    public Text repiarText;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Image newImage = Instantiate(original, itemsNeedPannel);
            newImage.sprite = itemNeeded.image;
        }
    }

    public void StartRepair()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        repiarText.enabled = false;
        repairParticles.Play();

        if (inventory.HasItem(itemNeeded, amount))
        {
            StartCoroutine(RepairingCR());
        }
        else
        {
            MessageManager.getInstance().DissplayMessage("Not enough items invetory.", 3f);
        }
    }

    public void StopRepair()
    {
        repiarText.enabled = true;
        repairParticles.Stop();

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

        RepiaredItem();

        Destroy(gameObject);

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

    public virtual void RepiaredItem()
    {
        PlayerInventory player = FindObjectOfType<PlayerInventory>();
        player.RemoveItems(itemNeeded, amount);
    }
}