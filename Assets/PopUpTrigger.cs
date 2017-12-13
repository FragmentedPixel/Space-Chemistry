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

        while(currentIndex < imagesToDisplay.Length)
        {
            if (Input.anyKeyDown)
            {
                currentIndex++;
                if (currentIndex < imagesToDisplay.Length)
                    PopUpManager.instance.RequestPopUp(imagesToDisplay[currentIndex]);
                else
                    PopUpManager.instance.RequestPopUp(null);
            }

            yield return null;
        }

        yield break;
    }

}
