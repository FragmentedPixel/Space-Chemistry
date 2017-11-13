using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersManager : MonoBehaviour
{
    
    public Container[] GetContainers()
    {
        Container[] containers =  GetComponentsInChildren<Container>();

        if(containers.Length != 3)
        {
            Debug.LogError("There are not 3 containers.");
        }

        return containers;
    }

}
