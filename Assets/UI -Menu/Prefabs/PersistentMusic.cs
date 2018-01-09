using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Responsible for playing music through the game.
 */

public class PersistentMusic : MonoBehaviour
{
    #region Variabiles
    public AudioClip backgroundLoop;

    private AudioSource audioS;
    #endregion

    #region Initialization
    private void Start()
    {
        StartMusic();
        DontDestroyOnLoad(this);
    }


    private void StartMusic()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = backgroundLoop;
        audioS.volume = PlayerPrefsManager.GetMasterVolume()/10;
        audioS.Play();
    }
    #endregion
}
