using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null && Hit.gameObject.layer != LayerMask.NameToLayer("Solid"))
        {
            transform.GetComponentInParent<LiquidPool>().Splash(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y * Hit.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }
}
