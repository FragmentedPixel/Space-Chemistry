using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContanterMixer : MonoBehaviour
{

    //TODO: Change highlight better.

    public void Mix(Container first, Container second, Container third)
    {
        if(first.substance == null && first.particules == 0)
        {
            MixContainers(second, third, first);
        }

        else if (second.substance == null && second.particules == 0)
        {
            MixContainers(first, third, second);
        }

        else if (third.substance == null && third.particules == 0)
        {
            MixContainers(first, second, third);
        }

        else
        {
            MessageManager.getInstance().DissplayMessage("All containers are full.", 1.5f);
        }
        
    }

    private void MixContainers(Container cont1, Container cont2, Container destContainer)
    {

        sSubstance result = cont1.substance.CollidingWith(cont2.substance);

        if (result == null)
        {
            MessageManager.getInstance().DissplayMessage("Substances can't mix", 1f);
        }

        else
        {
            //TODO: Implement containers logics.
            float resultPart = cont1.particules + cont2.particules;

            //TODO: Update script name & class
            //TODO: Check if the mixed containers contains anything.

            cont1.EmptyContainer();
            cont2.EmptyContainer();

            destContainer.FillWith(resultPart, result);
        }
    }
}
