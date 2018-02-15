using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceGenerator : Generator
{

    #region Parameters
    // Magnitude of the random force applied at the spawn.
    public float randomForce;

    // State of the particle generated.
    public sSubstance particleSubstance;

    // Should the particle persist?
    public bool persistentParticle;
    #endregion

    private void Update()
    {
        Particle newParticle = CreateSubstance(particleSubstance);
        if(newParticle != null)
        {
            Vector3 randomVector = randomForce * Random.onUnitSphere;

            // Update particle parameters.
            newParticle.rb.AddForce(randomVector);

            // Make particle persist.
            if (persistentParticle)
                newParticle.MakeInfiniteLifeTime();
        }
    }

    public void SetSubstance(sSubstance newSubst)
    {
        particleSubstance = newSubst;
    }

    public void Enable()
    {
        enabled = true;
        audioS.Stop();
    }

    public void Disable()
    {
        enabled = false;
        audioS.Play();
    }
}
