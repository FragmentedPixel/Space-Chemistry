﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;
    public Image messageImage;

    private PauseMenu pauseMenu;

    private Sprite[] spritesToDisplay;
    private int spriteIndex;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void RequestPopUp(Sprite[] messagesToDisplay)
    {
        gameObject.SetActive(true);
        pauseMenu.SetPlayerControl(false);


        spritesToDisplay = messagesToDisplay;
        spriteIndex = 0;

        StartCoroutine(DisplayAll());
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
        pauseMenu.SetPlayerControl(true);
    }

    public void NextImage()
    {
        spriteIndex++;

        if (spriteIndex >= spritesToDisplay.Length)
            ClosePopUp();
    }

    public void PreviousImage()
    {
        spriteIndex--;

        if(spriteIndex <= 0)
        {
            spriteIndex = 0;
        }
    }

    private IEnumerator DisplayAll()
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

        ClosePopUp();

        yield break;
    }
}
