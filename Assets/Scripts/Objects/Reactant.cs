using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactant : MonoBehaviour
{
    public sSubstance reactantSubstance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Particle substance = collision.gameObject.GetComponent<Particle>();

        if(substance != null)
        {
            ReactWith(substance);
        }
    }

    protected virtual void ReactWith(Particle otherParticle)
    {
        otherParticle.ReactWith(reactantSubstance);
    }
}
