using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if(player != null)
        {
			CameraFollow camera = FindObjectOfType<CameraFollow>();
			camera.SetZoom(zoom);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
			CameraFollow camera = FindObjectOfType<CameraFollow>();
			camera.RemoveZoom ();
        }
    }

}
