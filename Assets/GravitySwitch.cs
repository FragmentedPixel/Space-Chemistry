using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private bool gravity = true;

    protected void SwitchAllRbs(bool on)
    {
        Rigidbody2D[] rbs = FindObjectsOfType<Rigidbody2D>();

        foreach(Rigidbody2D rb in rbs)
        {
            rb.gravityScale = on ? 1f : -.1f;
        }

        gravity = !gravity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
            SwitchAllRbs(gravity);
    }
}
