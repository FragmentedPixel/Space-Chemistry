using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for delegating the events to the current substance.
 */

public class Particle : MonoBehaviour
{
    #region Variabiles
    // All possible substances.
    public List<sSubstance> substanceList = new List<sSubstance>();

    // Current state of the substance.
    public sSubstance currentSubstance;

    // Has infinite life time ?
    public bool hasInfiniteLifeTime = false;

    // Time since the particle was created.
    private float currentLifeTime = 0f;

    // Total life time of the particle.
    private float totalLifeTime = 0f;

    // Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
	[HideInInspector] public SpriteRenderer rend;
    #endregion

    #region State Specific
    private void FixedUpdate()
    {
        // Runs the update on the scriptable object.
        if(currentSubstance !=  null)
        {
            currentSubstance.BehaviourUpdate(this);
        }
        
        // Return the particle to the pool when it dies.
        if(currentLifeTime > totalLifeTime && !hasInfiniteLifeTime)
        {
            ParticlePool.instance.ReturnParticle(this);
        }
        else
        {
            currentLifeTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The the other substance script.
        Particle otherParticle = collision.gameObject.GetComponent<Particle>();

        if (otherParticle != null)
        {
            // Handle the collision between them in the current state.
            sSubstance otherSubstance = otherParticle.currentSubstance;
            ReactWith(otherSubstance);
        }
    }

    public void ReactWith(sSubstance otherSubstance)
    {
        // Run the collision on the current substance scriptable object.
        sSubstance newSubstance = currentSubstance.CollidingWith(otherSubstance);

        // Update current substance if the answer is not null.
        if (newSubstance != null)
            ChangeSubstanceState(newSubstance);
    }
    #endregion

	#region Activation

	private void OnEnable()
	{
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
		rend = GetComponentInChildren<SpriteRenderer>();
	}

	public void Activate(sSubstance newSubstance)
	{
		ChangeSubstanceState (newSubstance);
		ChangeActiveState (true);
	}

	public void Deactivate()
	{
		ChangeActiveState (false);
	}

	private void ChangeActiveState(bool isOn)
	{
        // Disable Components.
		col.enabled = isOn;
        rb.velocity = Vector2.zero;

		// Make particle invisible.
		transform.localScale = isOn ? Vector3.one : Vector3.zero;

        // Disable Script
        enabled = isOn;
	}

	#endregion

    #region Helper Methods
    public void ChangeSubstanceState(sSubstance newSubstance)
    {
        // Set substance reference
        currentSubstance = newSubstance;

        // Update the life time.
        totalLifeTime = newSubstance.particleLifeTime;
        currentLifeTime = 0;

        // Set the rigidbody for the particle..
        rb.gravityScale = newSubstance.particleGravity;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.zero;

        // Move it to the corresponding layer.
        gameObject.layer = (int)newSubstance.particleLayer;

        // Update the material used by the mesh renderer.
        rend.material = newSubstance.particleMaterial;
    }

    public void MakeInfiniteLifeTime()
    {
        hasInfiniteLifeTime = true;
    }

    public float GetPercentagePassed()
    {
        if (!hasInfiniteLifeTime)
            return currentLifeTime / totalLifeTime;
        else
            return 0f;
    }

    public void ScaleDownSprite(Vector2 newScale)
    {
        rend.transform.localScale = newScale;
    }
    #endregion
}
