using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public Sprite[] imagesToDisplay;
    private int currentIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if(player != null)
        {
            StartCoroutine(DisplayAll());
        }
    }

    private IEnumerator DisplayAll()
    {
        PopUpManager.instance.RequestPopUp(imagesToDisplay[0]);
        yield return new WaitForSeconds(.3f);

        bool pressed = Input.GetAxis("Horizontal") == 0;

        while (currentIndex < imagesToDisplay.Length)
        {
            if(Input.GetAxis("Horizontal") < 0f && pressed == false)
            {
                pressed = true;
                currentIndex--;

                if(currentIndex < 0)
                {
                    currentIndex = 0;
                }

                else if (currentIndex < imagesToDisplay.Length)
                    PopUpManager.instance.RequestPopUp(imagesToDisplay[currentIndex]);
                else
                    PopUpManager.instance.RequestPopUp(null);
            }

            else if(Input.GetAxis("Horizontal") > 0f && pressed == false)
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

        PopUpManager.instance.RequestPopUp(null);

        yield break;
    }

}
