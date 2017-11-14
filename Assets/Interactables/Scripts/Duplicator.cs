using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
    public bool canChange;

    private bool alreadySet = false;

    private Generator emitter;

    private void Start()
    {
        emitter = GetComponentInChildren<Generator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadySet == false)
        {
            Particle particle = collision.gameObject.GetComponent<Particle>();
            emitter.SetSubstance(particle.currentSubstance);
            alreadySet = true;
        }
        else if(canChange == true)
        {
            Particle particle = collision.gameObject.GetComponent<Particle>();
            emitter.SetSubstance(particle.currentSubstance);
        }
        
    }

}
