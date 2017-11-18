using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Responsible for managing the player's containers.
 */

public class Container : MonoBehaviour
{
    #region Variabiles
    public sSubstance substance;
    public float particules;
    public float capacity = 15;

    public Image highLightImage;
    public Image fillImage;
    #endregion

    #region UI Updates

    public void UpdateContainerSubstance(sSubstance newSubstance)
    {
        substance = newSubstance;
    }

    public void UpdateContainerUI()
    {
        fillImage.fillAmount = particules / capacity;

        if(substance != null)
            fillImage.color = substance.particleColor;
    }

    #endregion

    #region Fill & Release
    public bool AddParticule(sSubstance substanceParticule)
    {
        // Update substance if none is inside.
        if(substance == null)
        {
            particules = 1;
            substance = substanceParticule;
            return true;
        }
        // Check if there is the right substance.
        else if (substance == substanceParticule)
        {
            particules++;
            return true;
        }
        // Let the player know if the wrong substance is inside.
        else
        {
            MessageManager.getInstance().DissplayMessage("There is an other substance inside this container.", 1f);
            return false;
        }
    }

    public sSubstance ReleaseParticule()
    {
        // Check if there is any substance in the container.
        if(substance == null)
        {
            return null;
        }

        // Check if there is any substance left inside the container.
        else if (particules == 0)
        {
            substance = null;
            return null;
        }

        // Return particle of the substance.
        else
        {
            particules--;
            return substance;
        }
    }
    #endregion

    #region Empty & Fill Container
    public void EmptyContainer()
    {
        // Clear all data about the container.
        particules = 0;
        substance = null;

        UpdateContainerUI();
    }

    public void FillWith(float particulesCount, sSubstance newSubstance)
    {
        particules = particulesCount;
        substance = newSubstance;

        UpdateContainerUI();
    }

    public bool isFull()
    {
        return (capacity <= particules);
    }

    public bool isEmpty()
    {
        if (substance == null)
            return true;
        else if (particules <= 0)
            return true;
        else
            return false;
    }
    #endregion

    #region Highlighting
    public void HighLight()
    {
        SetImageAlpha(1f);
    }

    public void StopHighLight()
    {
        SetImageAlpha(0f);
    }

    private void SetImageAlpha(float alpha)
    {
        Color highColor = highLightImage.color;
        highColor.a = alpha;
        highLightImage.color = highColor;
    }
    #endregion
}
