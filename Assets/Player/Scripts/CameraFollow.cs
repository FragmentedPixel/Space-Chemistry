using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public float smoothSpeed = 12.5f;
	public float zoomSpeed = 1f;
    public float moveThreshold = 1f;

    private Transform target;
    private Transform player;

    private float currentMovement;

    private Vector3 lastPlayerPosition;
    private Vector3 lastTargetPosition;

    public Vector3 offset;
	private float initZoom;

	private void Start()
	{
		PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        player = playerMov.transform;
		target = playerMov.cameraPoint;

        transform.position = player.position + offset;


        initZoom = GetComponent<Camera>().orthographicSize;
	}

	void FixedUpdate()
	{
        float currentMovement = (lastPlayerPosition - player.position).magnitude;
        if(currentMovement > moveThreshold)
        {
            lastPlayerPosition = player.position;
            lastTargetPosition = target.position;
        }

 		Vector3 desiredPosition = lastTargetPosition + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);

		transform.position = smoothedPosition;
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
