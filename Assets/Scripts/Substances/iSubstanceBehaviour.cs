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
    #endregion

    #region Specific

    public virtual void ActivateState()
    {
        Debug.Log("Update on base class.");
    }

    public virtual void Update()
    {
        Debug.Log("Running Update on base class.");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Substance otherSubstance = collision.gameObject.GetComponent<Substance>();

        if (otherSubstance != null)
        {
            State newState = HandleSubstanceCollision(otherSubstance.substanceState);

            if (newState != State.NONE)
                GetComponentInParent<Substance>().UpdateSubstanceState(newState);
        }
    }

    public virtual State HandleSubstanceCollision(State otherSubstance)
    {
        Debug.Log("Substance Collision on base level.");

        return State.NONE;
    }

    
    #endregion

    #region Movement & Scale

    // This scales the particle image acording to its velocity, so it looks like its deformable... but its not ;)
    public void MovementAnimation()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();

        Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);			
        movementScale.x += Mathf.Abs(rb.velocity.x) / 30.0f;
        movementScale.z += Mathf.Abs(rb.velocity.y) / 30.0f;
        movementScale.y = 1.0f;

        transform.localScale = movementScale;
    }

    // The effect for the particle to seem to fade away
    public void ScaleDown()
    {
        //TODO: Refactor this.

        float timePassed = GetComponentInParent<Substance>().currentLifeTime;

        float scaleValue = 1.0f - ( timePassed / particleLifeTime);
        Vector2 particleScale = Vector2.one;
        
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        transform.localScale = particleScale;
        
    }

    #endregion
}

