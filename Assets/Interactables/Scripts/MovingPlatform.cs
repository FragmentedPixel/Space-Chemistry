using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
	public Transform wayPointsparent;
	public float speed = 5f;
	public float threshhold = .3f;

	public int currentIndex = 0;

	private void Update()
	{
		Vector3 direction =  wayPointsparent.GetChild (currentIndex).position - transform.position;

		if(direction.magnitude < threshhold)
		{
			currentIndex = (currentIndex + 1) % wayPointsparent.childCount;
		}
		else
		{
			direction.Normalize ();
			transform.position = Vector3.Lerp (transform.position, wayPointsparent.GetChild (currentIndex).position, Time.deltaTime * speed);
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        player.transform.SetParent(transform);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        player.transform.SetParent(null);
    }
}
