using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Responsible for handling the pause menu input & commands.
 */

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isOn = false;

    #region Updating the Pannel
    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButtonDown("PauseMenu"))
        {
            isOn = !isOn;
            pausePanel.SetActive(isOn);
            SetPlayerControl(!isOn);

            if (isOn)
                StartCoroutine(HighLightBtnCR());
        }
    }

    public void SetPlayerControl(bool hasControl)
    {
        PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        PlayerContainers playerCont = playerMov.GetComponent<PlayerContainers>();
        HandMovement playerHand = playerMov.GetComponentInChildren<HandMovement>();

        playerMov.ChangeControl(hasControl);
        playerCont.ChangeControl(hasControl);
        playerHand.ChangeControl(hasControl);
    }

    IEnumerator HighLightBtnCR()
    {
        EventSystem myEventSystem = FindObjectOfType<EventSystem>();
        myEventSystem.SetSelectedGameObject(null);
        yield return null;
        myEventSystem.SetSelectedGameObject(myEventSystem.firstSelectedGameObject);
    }
    #endregion

    #region Button Commands
    public void Resume()
    {
        isOn = false;

        pausePanel.SetActive(isOn);
        SetPlayerControl(!isOn);
    }

    public void GoToMenu()
    {
        FindObjectOfType<LevelManager>().ChangeScene("Menu");
    }

    public void RestartLevel()
    {
        FindObjectOfType<LevelManager>().Reload();
    }

    public void ShowTutorials()
    {
        FindObjectOfType<TutorialsList>().DisplayTutorials();
    }
    #endregion
}
