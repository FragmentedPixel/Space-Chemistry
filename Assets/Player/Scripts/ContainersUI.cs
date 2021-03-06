﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for managing the containers from the UI and sending them to the player.
 */

public class ContainersUI : UIMenu
{
    private Container[] containers;

    protected override void Awake()
    {
        base.Awake();
        containers =  GetComponentsInChildren<Container>();
        gameObject.SetActive(true);

        foreach (Container cont in containers)
            cont.StopHighLight();

        var persistent = FindObjectOfType<PersistentContainers>();
        if(persistent!= null)
            persistent.LevelStarted();
    }

    public Container[] GetContainers(int containersCount)
    {
        if(containers.Length != containersCount)
        {
            Debug.LogError("There are not the same number of containers in UI and player.");
        }

        return containers;
    }

    public void LockAllBarrels()
    {
        foreach (Container cont in containers)
            cont.gameObject.SetActive(false);

		GetComponent<Image> ().enabled = false;
        PlayerContainers.availableContainers = 0; //Set all available containers to 0 for the player script
    }

    public void UnLockNextBarrel()
    {
		GetComponent<Image> ().enabled = true;
        
        for (int i = 0; i < containers.Length; i++)
        {
            if(!containers[i].gameObject.activeInHierarchy)
            {
                PlayerContainers.availableContainers++; //Unlock one more container for the player
                containers[i].gameObject.SetActive(true);
                containers[i].Unlock();
                return;
            }
        }
    }

    public bool AreContainersAvailable()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            if (containers[i].gameObject.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

}
