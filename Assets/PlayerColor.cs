using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public Color colorInPool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if(player != null)
        {
            Debug.Log("da");
            SpriteRenderer[] playerMesh = player.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sp in playerMesh)
            {
                sp.color = colorInPool;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("nu");

            SpriteRenderer[] playerMesh = player.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sp in playerMesh)
            {
                sp.color = Color.white;
            }
        }
    }


}
