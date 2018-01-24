﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRepaired : RepiarableItem
{
    public BoxCollider2D[] collidersToEnable;

    public override void RepiaredItem()
    {
        foreach(BoxCollider2D collider in collidersToEnable)
        {
            collider.enabled = true;
        }
    }

}