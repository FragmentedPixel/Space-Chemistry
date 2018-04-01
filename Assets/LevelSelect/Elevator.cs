using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 3f;

    private void OnEnable()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.DisableJump();
    }

    private void Update()
    {
        float input = Input.GetAxis("Vertical");

        transform.position += Vector3.up * input * speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 destination)
    {
        StopAllCoroutines();

        StartCoroutine(MoveToCR(destination));
    }

    private IEnumerator MoveToCR(Vector3 destination)
    {
        float stopThreshold = .1f;

        while(Vector3.Distance(transform.position, destination) > stopThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            yield return null;
        }

        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stick the player to the platform.
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
            player.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove the player from the platform.
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
            player.transform.SetParent(null);
    }
}
