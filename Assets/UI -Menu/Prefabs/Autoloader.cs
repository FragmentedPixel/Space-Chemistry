using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Responsible for loading the next scene after a sound is played.
 */

public class Autoloader : MonoBehaviour
{
    #region Variabiles
    // Name of the target scene.
    public string sceneName;

    // Time waited after the sound is finished.
    public float waitDelay = 0.5f;

    // Played at the start.
    public AudioClip introSound;

    // Used to play the sound.
    private AudioSource audioS;
    #endregion

    #region Loading Scene after sounds end
    private void Start()
    {
        // Plays the intro sound.
        PlayIntroSound();

        // Start the loading CR.
        StartCoroutine(LoadSceneCR());
    }

    private void PlayIntroSound()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = .1f;
        audioS.PlayOneShot(introSound);
    }

    private IEnumerator LoadSceneCR()
    {
        yield return new WaitForSeconds(introSound.length + waitDelay);
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
