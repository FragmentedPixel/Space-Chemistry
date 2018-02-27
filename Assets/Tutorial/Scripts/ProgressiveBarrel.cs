using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for unlocking the barrels through the level.
 */

public class ProgressiveBarrel : PlayerTriggerable
{
    private ContainersUI containersManager;
 
    private void Start()
    {
        containersManager = FindObjectOfType<ContainersUI>();
        containersManager.LockAllBarrels();
    }

    protected override void OnPlayerTriggered()
    {
        containersManager.UnLockNextBarrel();

        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }
}
