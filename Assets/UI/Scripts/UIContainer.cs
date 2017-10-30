using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Responsible for managing the player's containers.
 */

public class UIContainer : MonoBehaviour
{
    public sSubstance substance;
    public int particules;

    public Image highLightImage;
    public Image fillImage;

    public void UpdateContainer(Color newColor, float fillAmount)
    {
        fillImage.color = newColor;
        fillImage.fillAmount = fillAmount;
    }

    public void UpdateContainerColor(Color finalColor)
    {
        fillImage.color = finalColor;
    }

    public void HighLight()
    {
        Color highColor = highLightImage.color;
        highColor.a = 1;
        highLightImage.color = highColor;
    }

    public void StopHighLigh()
    {
        Color highColor = highLightImage.color;
        highColor.a = 0;
        highLightImage.color = highColor;
    }
}
