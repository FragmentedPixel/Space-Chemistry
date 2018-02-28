using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//TODO: Newst on top.

public class MenuUIManager : MonoBehaviour
{
    #region Variabiles
    public UIMenu pauseMenu;
    public UIMenu skillMenu;
    public UIMenu tutorialMenu;

    private List<UIMenu> menus;

    private bool playerHasControl = false;

    private int zIndex = 0;
    #endregion

    #region Menu Handeling

    private void Start()
    {
        // Ads all the menus to the menu list.
        PopulateMenus();
    }

    private void PopulateMenus()
    {
        // Create a new list and add all the menus to it.
        menus = new List<UIMenu>();
        menus.Add(skillMenu);
        menus.Add(pauseMenu);
        menus.Add(tutorialMenu);
    }

    private void CheckMenu(UIMenu menuToCheck)
    {
        // Checks if the menu is already open
        if (menuToCheck.isOpen == false)
            OpenMenu(menuToCheck);
        else
            CloseMenu(menuToCheck);
    }

    private void OpenMenu(UIMenu menuToOpen)
    {
        // Opens a specific menu
        menuToOpen.Activate();

        // Removes the player control.
        SetPlayerControl(false);
    }

    private void CloseMenu(UIMenu menuToClose)
    {
        // Closes a specific menu
        menuToClose.Deactivate();

        // Gives the player the control back if there are no menus open.
        if (AreAllMenusClosed() == true)
            SetPlayerControl(true);
    }

    private void CloseAllMenus()
    {
        // Closes all the menus
        foreach (UIMenu menu in menus)
            menu.Deactivate();

        SetPlayerControl(true);
    }

    private bool AreAllMenusClosed()
    {
        // Checks if all the menus are closed.
        foreach (UIMenu menu in menus)
            if (menu.isOpen == true)
                return false;

        return true;
    }

    #endregion

    #region Player Control
    public void SetPlayerControl(bool hasControl)
    {
        playerHasControl = hasControl;
        Cursor.visible = !hasControl;

        // TODO: Disable all player contrables at once.
        PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        PlayerContainers playerCont = playerMov.GetComponent<PlayerContainers>();
        HandMovement playerHand = playerMov.GetComponentInChildren<HandMovement>();
        ProjectilePath playerProjectile = playerMov.GetComponentInChildren<ProjectilePath>();

        playerMov.ChangeControl(hasControl);
        playerCont.ChangeControl(hasControl);
        playerHand.ChangeControl(hasControl);
        playerProjectile.ChangeControl(hasControl);


        //TODO: Check this one out.
        // It highlights the first button in the menu if there is no mouse.
        //StartCoroutine(HighLightBtnCR());
    }

    IEnumerator HighLightBtnCR()
    {
        EventSystem myEventSystem = FindObjectOfType<EventSystem>();
        myEventSystem.SetSelectedGameObject(null);
        yield return null;
        myEventSystem.SetSelectedGameObject(myEventSystem.firstSelectedGameObject);
    }
    #endregion

    #region ActionToPerform
    private void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            CheckMenu(pauseMenu);   
        }

        else if(Input.GetButtonDown("SkillMenu"))
        {
            CheckMenu(skillMenu);
        }
    }

    public void ResumeGame()
    {
        CloseAllMenus();
    }

    public void DisplayTutorials()
    {
        CheckMenu(tutorialMenu);
    }
    #endregion
}
