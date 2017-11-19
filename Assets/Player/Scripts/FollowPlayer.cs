using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Transform player;

    private void Start()
    {
        PlayerMovement playerMov = FindObjectOfType<PlayerMovement>();
        target = playerMov.cameraPoint;
        player = playerMov.transform;
    }

    private void Update()
    {
        //TODO: Boundary when the player reaches the camera.
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), Time.deltaTime);
       
        // Pentru cand iti dai seama ca are stutterstep.
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, -10f), speed * Time.deltaTime);
    }

}
