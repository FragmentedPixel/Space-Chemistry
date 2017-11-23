using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstancesOptimizator : MonoBehaviour
{
    private void Awake()
    {
        Generator[] generators = FindObjectsOfType<Generator>();
        foreach(Generator gen in generators)
        {
            gen.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Generator generator = collision.gameObject.GetComponent<Generator>();
        if(generator != null)
        {
            generator.enabled = true;
        }
    }
}
