using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public RepiarableItem repairedItem;

    private void Update()
    {
        if(Input.GetButtonDown("Repair"))
        {
            FindObjectToRepair();
        }

        if(Input.GetButtonUp("Repair"))
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
            Debug.Log("stoppedRepairing");
            repairedItem.StopRepair();
        }
    }
}
