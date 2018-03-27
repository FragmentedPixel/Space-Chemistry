using System;
using System.Collections.Generic;
using UnityEngine;

public class CullingManager : MonoBehaviour
{
    public List<CulledLevel> levels = new List<CulledLevel>();

    public int levelsLoadedAtStart = 0;
    private int currentIndex = 0;


    public void Start()
    {
        foreach (CulledLevel level in levels)
            level.Disable();

        for (int i = 0; i < levelsLoadedAtStart; i++)
            levels[i].Enable();

        
    }

    public void LoadNextLevel()
    {
        levels[currentIndex].Disable();

        int indexToLoad = currentIndex + levelsLoadedAtStart;

        if (indexToLoad < levels.Count)
            levels[indexToLoad].Enable();

        currentIndex++;
    }


}


[Serializable]
public class CulledLevel
{
    public GameObject LevelParent;
    
    public void Disable()
    {
        LevelParent.SetActive(false);
    }

    public void Enable()
    {
        LevelParent.SetActive(true);
    }

}

