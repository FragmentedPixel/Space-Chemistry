using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersManager : MonoBehaviour
{
    
    public Container[] GetContainers(int containersCount)
    {
        Container[] containers =  GetComponentsInChildren<Container>();

        if(containers.Length != containersCount)
        {
            Debug.LogError("There are not the same number of containers in UI and player.");
        }

        return containers;
    }

}
