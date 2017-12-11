using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float minMovement = 3f;
    public float stopThreshold = .05f;
    public float countingThreshold = .1f;

    [Header("Zooming")]
    public float zoomSpeed;
    private float initZoom;
    
    private Transform player;
    private Transform target;
    private Vector3 lastPlayerPosition;
    private float currentMovement = 0f;


    private void Start()
    {
        PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        target = playerMov.cameraPoint;

        player = playerMov.transform;
        lastPlayerPosition = player.position;

        currentMovement = minMovement;

        initZoom = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        // Check if the player moved enough to move
        float playerMovementDiffernece = (player.position - lastPlayerPosition).magnitude;
        currentMovement = Mathf.Abs(playerMovementDiffernece);

        if(currentMovement > minMovement)
        {
            Debug.Log(currentMovement);
            // Camera should move
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);

            // Check if the camera hasn't reached the destination.
            float distanceLeft = (transform.position - targetPosition).magnitude;
            if (distanceLeft > stopThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            }

            // Reset the current movement if the camera is close enough.
            if(distanceLeft < countingThreshold)
            {
                lastPlayerPosition = player.position;
            }
        }
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
}
