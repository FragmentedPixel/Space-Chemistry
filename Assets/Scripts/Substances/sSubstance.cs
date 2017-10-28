using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewSubstance", menuName = "Substance/New Basic Substance")]

[Serializable]

public class sSubstance : ScriptableObject
{
    //TODO: Make all objects use the substance script as parameter.
   
    // Layer of the particle
    public enum SubstanceLayer { Fluids = 8, Steam = 9 };

    #region Variabiles
    [Header("General")]
    public SubstanceLayer particleLayer;

    // Gravity particle
    public float particleGravity;

    // Material of the particle.
    public Material particleMaterial;

    // How much time before the particle scales down and dies
    public float particleLifeTime = 3.0f;

    public List<Reaction> reactions;
    #endregion

    #region Specific
    public sSubstance CollidingWith(sSubstance otherSubstance)
    {
        for(int i = 0; i < reactions.Count; i++)
        {
            if (reactions[i].secondSubstance == otherSubstance)
                return reactions[i].resultSubstance;
        }

        return null;
    }

    public virtual void BehaviourUpdate(Particle substanceScript)
    {
        Debug.Log("Running Update on base class.");
    }

    #endregion

    #region Movement & Scale

    public void MovementAnimation(Transform transform, Rigidbody2D rb)
    {
        // This scales the particle image according to its velocity, so it looks like its deformable

        Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);
        movementScale.x += Mathf.Abs(rb.velocity.x) / 30.0f;
        movementScale.z += Mathf.Abs(rb.velocity.y) / 30.0f;
        movementScale.y = 1.0f;

        transform.localScale = movementScale;
    }

    public void ScaleDown(Particle substanceScript)
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = 1.0f - percentagePassed;
        Vector2 particleScale = Vector2.one;

        // Update the particle scale.
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        substanceScript.transform.localScale = particleScale;
    }

    #endregion
}

[Serializable]
public class Reaction
{
    public sSubstance secondSubstance;
    public sSubstance resultSubstance;
}
