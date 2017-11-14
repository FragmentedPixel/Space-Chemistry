using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoryer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Particle particle = col.gameObject.GetComponent<Particle>();

        if(particle != null)
        {
            ParticlePool.instance.ReturnParticle(particle);
        }
    }
}
