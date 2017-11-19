using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool mirceaChoice = false;
    public float speed;
    private Transform target;

    private void Start()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        target = player.cameraPoint;
    }

    private void Update()
    {
        if(mirceaChoice == true)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, -10f), speed * Time.deltaTime);
    }

}
