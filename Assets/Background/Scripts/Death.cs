using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(ExecuteAfterTime(1));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {

            yield return new WaitForSeconds(time);
            levelManager.RespawnPlayer();
        
    }
}
