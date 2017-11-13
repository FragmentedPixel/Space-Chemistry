using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersManager : MonoBehaviour
{
    
    public void SetContainers(Container container1, Container container2, Container container3)
    {
        Container[] containers =  GetComponentsInChildren<Container>();

        if(containers.Length != 3)
        {
            Debug.LogError("There are not 3 containers.");
        }

        container1 = containers[0];
        container2 = containers[1];
        container3 = containers[2];
    }

}
