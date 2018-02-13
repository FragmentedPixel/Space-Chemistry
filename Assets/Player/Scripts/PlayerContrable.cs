using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrable : MonoBehaviour
{
    private bool hasControl;

    public void ChangeControl(bool hasControl)
    {
        if (hasControl == false)
            RemoveControl();

        enabled = hasControl;
    }

    public virtual void RemoveControl()
    {
        Debug.Log("Removing control on base class.");
    }

}
