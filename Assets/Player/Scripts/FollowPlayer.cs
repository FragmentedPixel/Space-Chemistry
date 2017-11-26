using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float movementSpeed;
    public float zoomSpeed;

    private Transform target;
    private Transform player;

    private float initZoom;

    private void Start()
    {
        PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        target = playerMov.cameraPoint;
        player = playerMov.transform;

        initZoom = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        //TODO: Solve problem when player is moving backwards.
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), Time.deltaTime * movementSpeed);
    }

    public void SetZoom(float newZoom)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomLerpCR(newZoom));
    }

    public void RemoveZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomLerpCR(initZoom));
    }

    private IEnumerator ZoomLerpCR(float targetZoom)
    {
        Camera currentCamera = GetComponent<Camera>();
        while(currentCamera.orthographicSize != targetZoom)
        {
            currentCamera.orthographicSize = Mathf.Lerp(currentCamera.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    public void FocusOnPlayer()
    {
        Vector3 focusPosition = player.position;
        focusPosition.z = -10f;
        transform.position = focusPosition;
    }

    public bool isCameraInFrontOf(Vector3 testPosition)
    {
        if (transform.position.x > testPosition.x)
            return true;
        if (transform.position.y > testPosition.y)
            return true;

        return false;
    }



}
