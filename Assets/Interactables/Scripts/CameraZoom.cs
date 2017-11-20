using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom = 5;
    private float initzoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if(player != null)
        {
            FollowPlayer[] cameras = FindObjectsOfType<FollowPlayer>();
            initzoom = cameras[0].GetComponent<Camera>().orthographicSize;
            foreach(FollowPlayer camera in cameras)
            {
                camera.GetComponent<Camera>().orthographicSize = zoom;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            FollowPlayer[] cameras = FindObjectsOfType<FollowPlayer>();
            foreach (FollowPlayer camera in cameras)
            {
                camera.GetComponent<Camera>().orthographicSize = initzoom;
            }
        }
    }

}
