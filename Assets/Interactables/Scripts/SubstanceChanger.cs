using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceChanger : MonoBehaviour {
    private Generator generatorSubstance;
    public sSubstance changedSubstance;
    public GameObject gameObjectToBeDestroyed;

	// Use this for initialization
	void Start () {
        generatorSubstance = gameObject.GetComponent<Generator>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameObjectToBeDestroyed == null)
        {
            generatorSubstance.particleSubstance = changedSubstance;
        }
	}
}
