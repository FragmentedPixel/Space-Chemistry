using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactant : MonoBehaviour
{
    public State reactantState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Substance substance = collision.gameObject.GetComponent<Substance>();

        if(substance != null)
        {
            ReactWith(substance);
        }
    }

    protected virtual void ReactWith(Substance otherState)
    {
        Debug.Log("Reacting on a base level.");
    }
}
