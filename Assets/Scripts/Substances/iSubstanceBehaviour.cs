using UnityEngine;
using System;

// Base class for all substances.

[Serializable]
public class iSubstanceBehaviour : MonoBehaviour
{
    #region Variabiles
    [Header("General")]
    // State of the particle.
    public State particleState;

    // Gravity particle
    public float particleGravity;

    // How much time before the particle scales down and dies
    public float particleLifeTime = 3.0f;

    // Layer of the particle
    public enum SubstanceLayer { Fluids = 8, Steam = 9};
    
    public SubstanceLayer particleLayer;

    // Holds the parent's Rigidbody.
    protected Rigidbody2D rb;

    // Holds the parent's substance script.
    protected Substance substanceScript;
    #endregion

    #region Specific
    private void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        substanceScript = GetComponentInParent<Substance>();
    }

    public void ActivateState()
    {
        // Set the gravity for this particle.
        rb.gravityScale = particleGravity;

        // Move it to the corresponding layer.
        transform.parent.gameObject.layer = (int) particleLayer;

        //Update total life time in script.
        substanceScript.UpdateTotalLifeTime(particleLifeTime);
    }

    public virtual void Update()
    {
        Debug.Log("Running Update on base class.");
    }

    public virtual State CollidingWith(State otherSubstance)
    {
        Debug.Log("Substance Collision on base level.");

        return State.NONE;
    }

    #endregion

    #region Movement & Scale

    public void MovementAnimation()
    {
        // This scales the particle image according to its velocity, so it looks like its deformable

        Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);			
        movementScale.x += Mathf.Abs(rb.velocity.x) / 30.0f;
        movementScale.z += Mathf.Abs(rb.velocity.y) / 30.0f;
        movementScale.y = 1.0f;

        transform.localScale = movementScale;
    }

    public void ScaleDown()
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = 1.0f - percentagePassed;
        Vector2 particleScale = Vector2.one;
        
        // Update the particle scale.
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        transform.localScale = particleScale;
    }

    #endregion
}

