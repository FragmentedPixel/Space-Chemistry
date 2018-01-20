using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public Transform target;

	public float smoothSpeed = 12.5f;
	public Vector3 offset;

	private float initZoom;
	public float zoomSpeed = 1f;

	private void Start()
	{
		PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
		target = playerMov.cameraPoint;

		offset = transform.position - target.position;
		offset.x = offset.y = 0f;
		initZoom = GetComponent<Camera>().orthographicSize;
	}

	void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
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
