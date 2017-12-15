using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsList : MonoBehaviour
{
    public Sprite[] imagesToDisplay;
    private int currentIndex = 0;

    public void DisplayTutorials()
    {
        StartCoroutine(DisplayAll());
    }

    private IEnumerator DisplayAll()
    {
        PopUpManager.instance.RequestPopUp(imagesToDisplay[0]);

        bool pressed = false;

        while (currentIndex < imagesToDisplay.Length)
        {
            if (Input.GetAxis("Horizontal") < 0f && pressed == false)
            {
                pressed = true;
                currentIndex--;

                if (currentIndex < 0)
                {
                    currentIndex = 0;
                }

                else if (currentIndex < imagesToDisplay.Length)
                    PopUpManager.instance.RequestPopUp(imagesToDisplay[currentIndex]);
                else
                    PopUpManager.instance.RequestPopUp(null);
            }

            else if (Input.GetAxis("Horizontal") > 0f && pressed == false)
            {
                pressed = true;
                currentIndex++;
                if (currentIndex < imagesToDisplay.Length)
                    PopUpManager.instance.RequestPopUp(imagesToDisplay[currentIndex]);
                else
                    PopUpManager.instance.RequestPopUp(null);
            }

            else if (Input.anyKeyDown && pressed == false)
            {
                pressed = true;

                currentIndex++;
                if (currentIndex < imagesToDisplay.Length)
                    PopUpManager.instance.RequestPopUp(imagesToDisplay[currentIndex]);
                else
                    PopUpManager.instance.RequestPopUp(null);
            }

            else if (pressed == true && Input.GetAxis("Horizontal") == 0f)
            {
                pressed = false;
            }

            yield return null;
        }

        currentIndex = 0;

        PopUpManager.instance.RequestPopUp(null);

        yield break;
    }

}
