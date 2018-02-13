using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvetoryImage : MonoBehaviour
{
    public Image displayImage;
    public Text countText;

    public void SetUp(Sprite itemSprite, int itemCount)
    {
        displayImage.sprite = itemSprite;
        countText.text = "x" + itemCount;
    }
}
