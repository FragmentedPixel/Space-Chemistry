using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceGenerator : MonoBehaviour
{
    public float spawnInterval = 0.025f; // How much time until the next particle spawns
    private float lastSpawnTime = float.MinValue; //The last spawn time
    
    public Vector3 particleForce; //Is there a initial force particles should have?
    public State particlesState;

    public Transform particlesParent; // Where will the spawned particles will be parented (To avoid covering the whole inspector with them)
    public GameObject ParticulePrefab;

    void Update()
    {
        if (lastSpawnTime + spawnInterval < Time.time)
        {
            // Is it time already for spawning a new particle?

            GameObject newObject = Instantiate(ParticulePrefab);

            newObject.GetComponent<Rigidbody2D>().AddForce(particleForce);
            newObject.GetComponent<Substance>().UpdateSubstanceState(particlesState);
            newObject.transform.position = transform.position;
            newObject.transform.parent = particlesParent;
			
            lastSpawnTime = Time.time; // Register the last spawnTime			
        }
    }
}
