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
    public int pressesNeeded = 5;
    public Transform[] locations;
    private int lastLocationIndex;

    [Header("Repair Panel")]
    public Canvas repiarCanvas;
    public Image repairLocations;
    public Text repiarText;
    public Image repairImage;

    [Header("OverView Panel")]
    public Canvas overviewCanvas;
    public InvetoryImage invetoryImagePrefab;
    public Transform itemsNeedPannel;

    [Header("Button Animation")]
    private bool goingdown;
    private float currentTime = 0f;
    private float maxTime = 1f;
    private Vector3 maxScale;
    private Vector3 minScale;


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

        repiarCanvas.enabled = false;

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
        isRepairingNow = false;
        
        StopAllCoroutines();
    }
    
    private IEnumerator RepairingCR()
    {
        isRepairingNow = true;

        int currentPressed = 0;

        while (isRepairingNow && currentPressed < pressesNeeded)
        {
            if (Input.GetButtonDown("Repair"))
            {
                currentPressed ++;
                if(currentPressed < pressesNeeded)
                    repairImage.transform.position = GetRandomLocation().position;
            }

            yield return null;
        }

        isRepairingNow = false;

        if (currentPressed >= pressesNeeded)
            OnItemRepaired();
        else
            StopRepair();

        yield break;
    }

    private Transform GetRandomLocation()
    {
        int currentLocationIndex = 0;
        do
        {
            currentLocationIndex = Random.Range(0, locations.Length);

        } while (currentLocationIndex == lastLocationIndex);

        lastLocationIndex = currentLocationIndex;
        return locations[currentLocationIndex];

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
            isRepairingNow = false;
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

        Destroy(this);
    }
}

[System.Serializable]
public class ItemNeeded
{
    public RepairItem item;
    public int amount;
}