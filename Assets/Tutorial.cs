using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject[] turnon;

    public void Start()
    {
            foreach (GameObject g in turnon)
                g.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("sad");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Aici");
            foreach (GameObject g in turnon)
                g.SetActive(true);
        }
    }
}
