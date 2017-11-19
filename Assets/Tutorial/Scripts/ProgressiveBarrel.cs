using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveBarrel : MonoBehaviour
{
    private ContainersManager containersManager;

    private void Start()
    {
        containersManager = FindObjectOfType<ContainersManager>();
        containersManager.LockAllBarrels();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if(player != null)
        {
            containersManager.UnLockNextBarrel();
        }
    }
}
