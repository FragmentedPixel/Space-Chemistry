﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstancesOptimizator : MonoBehaviour
{
    private void Awake()
    {
        SubstanceGenerator[] generators = FindObjectsOfType<SubstanceGenerator>();
        foreach(SubstanceGenerator gen in generators)
        {
            gen.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SubstanceGenerator[] generators = collision.gameObject.GetComponentsInChildren<SubstanceGenerator>();
        if(generators.Length != 0)
        {
            foreach(SubstanceGenerator generator in generators)
                generator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SubstanceGenerator[] generators = collision.gameObject.GetComponentsInChildren<SubstanceGenerator>();
        if (generators.Length != 0)
        {
            foreach (SubstanceGenerator generator in generators)
                generator.enabled = false;
        }
    }
}