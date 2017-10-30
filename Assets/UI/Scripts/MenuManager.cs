using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Responsible for navigating the main menu.
 */

public class MenuManager : MonoBehaviour
{
    #region Variabiles
    // Sound played at each button press.
    public AudioClip buttonSound;

    // AudioSource used for playing sounds.
    private AudioSource audioS;
    #endregion

    #region Initialization
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = .5f;
    }
    #endregion

    #region Changing Scene
    public void ChangeScene(string scene)
    {
        Time.timeScale = 1f;
        audioS.PlayOneShot(buttonSound);
        StartCoroutine(ChangeSceneCR(scene));
    }

    private IEnumerator ChangeSceneCR(string scene)
    {
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(scene);
        yield break;
    }
    #endregion

    #region Methods
    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}
