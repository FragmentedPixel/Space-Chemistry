using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsUI : UIMenu
{
    public Image messageImage;
    public Sprite[] spritesToDisplay;

    private int spriteIndex;

    private void OnEnable()
    {
        spriteIndex = 0;

        StartCoroutine(DisplayAllCR());
    }
    
    public void NextImage()
    {
        spriteIndex++;

        if (spriteIndex >= spritesToDisplay.Length)
            Deactivate();
    }

    public void PreviousImage()
    {
        spriteIndex--;

        if(spriteIndex <= 0)
        {
            spriteIndex = 0;
        }
    }

    private IEnumerator DisplayAllCR()
    {
        while(spriteIndex < spritesToDisplay.Length)
        {
            messageImage.sprite = spritesToDisplay[spriteIndex];

            if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                PreviousImage();
                yield return new WaitWhile(new System.Func<bool>(() => Input.GetAxisRaw("Horizontal") < 0f));
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                NextImage();
                yield return new WaitWhile(new System.Func<bool>(() => Input.GetAxisRaw("Horizontal") > 0f));
            }

            else if (Input.anyKeyDown)
            {
                NextImage();
            }

            yield return null;
        }

        Deactivate();

        yield break;
    }
}
