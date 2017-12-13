using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;
    public Image message;

    private PauseMenu pauseMenu;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void RequestPopUp(Sprite messageToDisplay)
    {
        if(messageToDisplay == null)
        {
            ClosePopUp();
        }
        else
        {
            message.sprite = messageToDisplay;
            gameObject.SetActive(true);
            pauseMenu.SetPlayerControl(false);

        }
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
        pauseMenu.SetPlayerControl(true);
    }
}
