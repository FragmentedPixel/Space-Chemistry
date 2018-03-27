using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : PlayerEnterExit
{
    public float zoom = 5;

    protected override void OnPlayerEnter()
    {
        CameraFollow camera = FindObjectOfType<CameraFollow>();
        camera.SetZoom(zoom);
    }

    protected override void OnPlayerExit()
    {
        CameraFollow camera = FindObjectOfType<CameraFollow>();
        camera.RemoveZoom();
    }
}
