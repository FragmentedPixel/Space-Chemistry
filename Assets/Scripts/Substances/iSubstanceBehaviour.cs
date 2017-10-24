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
    //TODO: Later transform this in single layer mask type or enum type.
    [Tooltip("Fluids = 8 Gas = 9")]
    public int particleLayer;
    #endregion

    #region Specific

    public void ActivateState()
    {
        // Set the gravity for this particle.
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = particleGravity;

        // Move it to the coresponding layer.
        transform.parent.gameObject.layer = particleLayer;
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
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();

        // This scales the particle image according to its velocity, so it looks like its deformable

        Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);			
        movementScale.x += Mathf.Abs(rb.velocity.x) / 30.0f;
        movementScale.z += Mathf.Abs(rb.velocity.y) / 30.0f;
        movementScale.y = 1.0f;

        transform.localScale = movementScale;
    }

    public void ScaleDown()
    {
        //TODO: Refactor this for OOP

        // The effect for the particle to seem to fade away
        float timePassed = GetComponentInParent<Substance>().currentLifeTime;

        float scaleValue = 1.0f - ( timePassed / particleLifeTime);
        Vector2 particleScale = Vector2.one;
        
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        transform.localScale = particleScale;
        
    }

    #endregion
}

