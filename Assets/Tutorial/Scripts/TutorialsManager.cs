using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    public Sprite[] imagesToDisplay;
    private int currentIndex = 0;

    public void DisplayTutorials()
    {
        PopUpManager.instance.RequestPopUp(imagesToDisplay);
    }
}
