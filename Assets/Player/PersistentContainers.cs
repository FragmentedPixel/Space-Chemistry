using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentContainers : MonoBehaviour
{
    ContainerData[] data = new ContainerData[2];
    
    public void LevelStarted()
    {
        var containerUI = FindObjectOfType<ContainersUI>();
        int index = 0;

        if (containerUI == null)
            return;

        foreach (Container cont in containerUI.GetContainers(2))
        {
            if(data[index] != null)
                cont.FillWith(data[index].count, data[index].subst);

            index++;
        }
    }

    public void LevelEnded()
    {
        var containerUI = FindObjectOfType<ContainersUI>();
        int index = 0;

        if (containerUI == null)
            return;

        foreach (Container cont in containerUI.GetContainers(2))
        {
            data[index] = new ContainerData(cont.substance, cont.particules);
            index++;
        }
    }

}

public class ContainerData
{
    public ContainerData(sSubstance _subst, float _count)
    {
        subst = _subst;
        count = _count;
    }

    public sSubstance subst;
    public float count;
}
