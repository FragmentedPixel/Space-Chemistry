using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for mixing the substances inside the backpack.
 */

public class ContanterMixer : MonoBehaviour
{
    public void Mix(Container first, Container second, Container third)
    {
        // Mix inside the empty container.

        if(first.isEmpty())
        {
            MixContainers(second, third, first);
        }

        else if (second.isEmpty())
        {
            MixContainers(first, third, second);
        }

        else if (third.isEmpty())
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
        // If there is one more empty container there is nothing to mix.
        if (cont1.isEmpty() || cont2.isEmpty())
            return;

        // Simulate the reaction between the 2 substances.
        sSubstance result = cont1.substance.CollidingWith(cont2.substance);

        // Check the result of the new substance.
        if (result == null)
        {
            MessageManager.getInstance().DissplayMessage("Substances can't mix", 1f);
        }

        else
        {
            // Calculate the number of the particules for the new container.
            float resultPart = cont1.particules + cont2.particules;
            
            // Empty the used containers.
            cont1.EmptyContainer();
            cont2.EmptyContainer();
            
            // Fill the destination container with the result.
            destContainer.FillWith(resultPart, result);
        }
    }
}
