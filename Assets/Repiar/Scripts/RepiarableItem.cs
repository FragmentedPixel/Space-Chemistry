using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepiarableItem : MonoBehaviour
{
    [Header("Repair Parameters")] 
    public float repiarDuration;
    public List<ItemNeeded> itemsNeeded;

    [Header("Quick Time event")]
    public float pressesNeeded = 100f;
    public float lostPerSecond = 5f;
    public float wonPerPress = 30f;

    [Header("Repair Feedback")]
    public Image repiarProgress;
    public ParticleSystem repairParticles;
    public Canvas repiarCanvas;
    public Canvas overviewCanvas;
    public InvetoryImage invetoryImagePrefab;
    public Transform itemsNeedPannel;
    public Text repiarText;
    public Image repairImage;

    private bool isPlayerInside;
    private bool isRepairingNow;

    private void Start()
    {
        // Display the items needed above the object.
        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            ItemNeeded currentItem = itemsNeeded[i];
            
            InvetoryImage invetoryImage = Instantiate(invetoryImagePrefab, itemsNeedPannel);
            invetoryImage.SetUp(currentItem.item.itemSprite, currentItem.amount);   
        }


        maxScale = repairImage.transform.localScale;
        minScale = .8f * maxScale;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Repair") && isPlayerInside == true && isRepairingNow == false)
        {
            StartRepair();
        }

        if (goingdown)
            repairImage.transform.localScale = Vector3.Lerp(maxScale, minScale, currentTime / maxTime);
        else
            repairImage.transform.localScale = Vector3.Lerp(minScale, maxScale, currentTime / maxTime);

        currentTime += Time.fixedDeltaTime;
        if (currentTime > maxTime)
        {
            goingdown = !goingdown;
            currentTime = 0f;
        }
    }

    public void StartRepair()
    {
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
        isRepairingNow = false;
        
        repiarProgress.fillAmount = 0f;
        StopAllCoroutines();
    }

    /*private IEnumerator RepairingCR()
    {
        isRepairingNow = true;
        float currentProgress = 0f;
        float progressNeeded = pressesNeeded * wonPerPress;

        float currentTime = 0f;
        while (currentTime <= repiarDuration)
        {
            repiarProgress.fillAmount = currentProgress / progressNeeded;

            currentProgress -= Time.deltaTime * lostPerSecond;
            if(Input.GetKeyDown(KeyCode.Q))
            {
                currentProgress += wonPerPress;
            }

            currentTime += Time.deltaTime;

            yield return null;
        }

        isRepairingNow = false;

        if (currentProgress >= progressNeeded)
            OnItemRepaired();
        else
            StopRepair();

        yield break;
    } */

    private bool goingdown;
    private float currentTime = 0f;
    private float maxTime = 1f;
    Vector3 maxScale;
    Vector3 minScale;
    
    private IEnumerator RepairingCR()
    {
        isRepairingNow = true;
        float currentProgress = wonPerPress;
        float progressNeeded = pressesNeeded * wonPerPress;

        

        while (currentProgress > 0f && currentProgress < progressNeeded)
        {
            repiarProgress.fillAmount = currentProgress / progressNeeded;

            currentProgress -= Time.deltaTime * lostPerSecond;
            if (Input.GetButtonDown("Repair"))
            {
                currentProgress += wonPerPress;
            }

            yield return null;
        }

        isRepairingNow = false;

        if (currentProgress >= progressNeeded)
            OnItemRepaired();
        else
            StopRepair();

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            isPlayerInside = true;
            repiarCanvas.enabled = true;
            overviewCanvas.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            isPlayerInside = false;
            repiarCanvas.enabled = false;
            overviewCanvas.enabled = true;
        }
    }

    public virtual void OnItemRepaired()
    {
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