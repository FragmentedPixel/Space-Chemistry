﻿using UnityEngine;
using System.Collections;
using System.Linq;

/*
 * Responsible for managing the player preferences.
 */

public class PlayerPrefsManager : MonoBehaviour
{
    #region Keys
    const string MASTER_VOLUME_KEY = "master_volume";
    const string QUALITY_LEVEL_KEY = "quality_level";
    const string RESOLUTION_KEY = "resolution_choice";
    const string FULLSCREEN_KEY = "fullscreen_choice";
    #endregion

    #region Methods 
    private void OnEnable()
    {
        Resolution startRes = Screen.resolutions[PlayerPrefsManager.GetResolution()];
        Screen.SetResolution(startRes.width, startRes.height, GetFullScreen());
    }

    #region Master Volume
    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 8f);
    }
    #endregion

    #region QualityLevel
    public static void SetQualityLevel(int value)
    {
        PlayerPrefs.SetInt(QUALITY_LEVEL_KEY, value);
    }

    public static int GetQualityLevel()
    {
        return PlayerPrefs.GetInt(QUALITY_LEVEL_KEY, 5);
    }
    #endregion

    #region Resolution
    public static void SetResolution(int value)
    {
        PlayerPrefs.SetInt(RESOLUTION_KEY, value);
    }

    public static int GetResolution()
    {
        return PlayerPrefs.GetInt(RESOLUTION_KEY, Screen.resolutions.Length - 1);
    }
    #endregion

    #region FullScreen
    public static void SetFullScreen(bool value)
    {
        int intValue = value ? 1 : 0;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, intValue);
    }

    public static bool GetFullScreen()
    {
        return (PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1);
    }
    #endregion

    #endregion
}