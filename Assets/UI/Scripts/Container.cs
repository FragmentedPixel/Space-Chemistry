using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Responsible for managing the player's containers.
 */

public class Container : MonoBehaviour
{
    public sSubstance substance;
    public float particules;
    public float capacity = 15;

    public Image highLightImage;
    public Image fillImage;
    
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

    #region Fill & Release
    public bool AddParticule(sSubstance substanceParticule)
    {
        if(substance == null)
        {
            particules = 1;
            substance = substanceParticule;
            return true;
        }
        else if (substance == substanceParticule)
        {
            particules++;
            return true;
        }
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
        fillImage.fillAmount = 0;
        particules = 0;
        substance = null;
    }

    public void FillWith(float particulesCount, sSubstance newSubstance)
    {
        particules = particulesCount;
        substance = newSubstance;

        // Update fill amount.
    }
    #endregion

    #region Highlighting

    public void HighLight()
    {
        SetImageAlpha(1f);
    }

    public void StopHighLigh()
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
