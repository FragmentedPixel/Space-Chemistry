using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for managing the containers from the UI and sending them to the player.
 */

public class ContainersManager : MonoBehaviour
{
    private Container[] containers;

    private void Awake()
    {
        containers =  GetComponentsInChildren<Container>();

        foreach (Container cont in containers)
            cont.StopHighLight();
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
    }

    public void UnLockNextBarrel()
    {
        for(int i = 0; i < containers.Length; i++)
        {
            if(!containers[i].gameObject.activeInHierarchy)
            {
                containers[i].gameObject.SetActive(true);
                containers[i].Unlock();
                return;
            }
        }
    }

}
