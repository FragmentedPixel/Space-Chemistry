using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Move particules inside a container to this class as well.

public class UIContainer : MonoBehaviour
{
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
