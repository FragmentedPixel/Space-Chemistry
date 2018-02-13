using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Repair Item", menuName = "Items/New Repair Item")]
public class RepairItem : ScriptableObject
{
    public Sprite itemSprite;
    public int count;

    private void OnEnable()
    {
        RepairItemsManager.AddItem(this);
        count = 0;
    }

    public void Collect()
    {
        count++;
        UpdateInvetory();   
    }

    public bool Has(int needed)
    {
        return (count >= needed);
    }

    public void Use(int needed)
    {
        count -= needed;
        UpdateInvetory();
    }

    private void UpdateInvetory()
    {
        RepairItemsManager.Update();
    }
}
