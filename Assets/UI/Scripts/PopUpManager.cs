using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;
    public Text messageText;

    private PauseMenu pauseMenu;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            ClosePopUp();
        }

        if(Input.anyKeyDown)
        {
            ClosePopUp();
        }
    }

    public void RequestPopUp(string messageToDisplay)
    {
        messageText.text = messageToDisplay;
        gameObject.SetActive(true);
        pauseMenu.SetPlayerControl(false);
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
        pauseMenu.SetPlayerControl(true);
    }
}
