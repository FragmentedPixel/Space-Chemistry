using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;
    public Sprite checkpointPassed;
    private SpriteRenderer spriterenderer;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            spriterenderer.sprite = checkpointPassed;
            levelManager.currentCheckpoint = gameObject;
        }
    }
}
