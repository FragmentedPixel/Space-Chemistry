using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for handling the pause menu input & commands.
 */

public class PauseMenu : UIMenu
{
    #region Button Commands
    public void Resume()
    {
        GetComponentInParent<UIManager>().ResumeGame();
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
		FindObjectOfType<TutorialsManager>().DisplayTutorials();
    }
    #endregion
}
