﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Responsible for navigating the main menu.
 */

public class LevelManager : MonoBehaviour
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
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }
    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Reload();
        }

        if(Input.GetKeyDown(KeyCode.F11))
        {
            LoadNextLevel();
        }
    }

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
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            ChangeScene("Menu");
    }
    #endregion
}
