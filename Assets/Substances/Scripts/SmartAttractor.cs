using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartAttractor : MonoBehaviour
{
    private List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    public float speed;

    private void Update()
    {
        foreach(Rigidbody2D rb in rbs)
        {
            Vector3 direction = (transform.position - rb.transform.position).normalized;
            rb.MovePosition(rb.transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Particle liquide = collision.gameObject.GetComponent<Particle>();
        if(liquide != null)
        {
            rbs.Add(liquide.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Particle liquide = collision.gameObject.GetComponent<Particle>();
        if (liquide != null)
        {
            rbs.Remove(liquide.GetComponent<Rigidbody2D>());
        }
    }

}
