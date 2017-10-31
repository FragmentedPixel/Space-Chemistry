using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        StartCoroutine(ExecuteAfterTime(1, player));

    }

    IEnumerator ExecuteAfterTime(float time, PlayerHealth player)
    {
        if (player)
        {
            yield return new WaitForSeconds(time);
            player.Death();
        }
    }
}
