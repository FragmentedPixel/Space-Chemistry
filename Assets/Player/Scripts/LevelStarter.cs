using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public GameObject UIprefab;
    public GameObject particleManagerPrefab;
    public GameObject levelManangerPrefab;

    private void Awake()
    {
        Instantiate(UIprefab, transform);
        Instantiate(playerPrefab, transform.position, transform.rotation, transform);
        Instantiate(particleManagerPrefab, transform);
        Instantiate(levelManangerPrefab, transform);
        Instantiate(cameraPrefab, transform.position, transform.rotation, transform);
    }
}
