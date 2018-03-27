using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            OnPlayerEnter();
        }
    }

    protected virtual void OnPlayerEnter()
    {
        Debug.Log("The player entered the trigger of the " + gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            OnPlayerExit();
        }
    }

    protected virtual void OnPlayerExit()
    {
        Debug.Log("The player exited the trigger of the " + gameObject.name);
    }

}
