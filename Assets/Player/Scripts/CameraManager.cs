using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;

    public Camera handCamera;
    public Camera liquidCamera;
    public Camera gameCamera;

    private Transform player;

    private void Awake()
    {
        cameraManager = this;
        player = FindObjectOfType<PlayerHealth>().transform;
    }

    public Camera GetHandCamera()
    {
        return handCamera;
    }
}
