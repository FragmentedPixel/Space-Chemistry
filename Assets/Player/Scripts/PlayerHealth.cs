using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform checkPoint;

    public void Death()
    {
        transform.position = checkPoint.position;
    }


}
