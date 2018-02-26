using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's death and respawning.
 */

public class PlayerHealth : MonoBehaviour
{
    #region Parameters
    [Range(0f,2f)] public float scale;
    public AudioClip deathSound;

    private AudioSource audioS;
    private Vector3 currentCheckpoint;
    private bool dead = false;

    private Animator anim;
    private ParticleSystem[] particles;
    #endregion

    #region CheckPoint
    private void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        audioS = gameObject.AddComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();

        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        foreach(ParticleSystem particle in particles)
        {
            var main = particle.main;
            ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(scale);
            main.startSize = curve;
        }
    }

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        currentCheckpoint = newCheckPoint;
    }

    public void Death()
    {
        if(dead == false)
        {
            audioS.PlayOneShot(deathSound);
            dead = true;
            anim.SetBool("Dead", true);

            PlayerMovement playerMov = GetComponent<PlayerMovement>();
            PlayerContainers playerCont = playerMov.GetComponent<PlayerContainers>();
            HandMovement playerHand = playerMov.GetComponentInChildren<HandMovement>();

            playerMov.ChangeControl(false);
            playerCont.ChangeControl(false);
            playerHand.ChangeControl(false);

            StartCoroutine(RespawnPlayerCR(deathSound.length));

        }
    }

    private void RespawnPlayer()
    {
        PlayerMovement playerMov = GetComponent<PlayerMovement>();
        PlayerContainers playerCont = playerMov.GetComponent<PlayerContainers>();
        HandMovement playerHand = playerMov.GetComponentInChildren<HandMovement>();

        playerMov.ChangeControl(true);
        playerCont.ChangeControl(true);
        playerHand.ChangeControl(true);
    }

    IEnumerator RespawnPlayerCR(float time)
    {
        yield return new WaitForSeconds(.1f);

        while(anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(time);
        anim.SetBool("Dead", false);
        
        transform.position = currentCheckpoint;

        yield return new WaitForSeconds(.3f);

        while(anim.GetCurrentAnimatorStateInfo(0).IsName("Respawn"))
        {
            yield return null;
        }
        
        RespawnPlayer();

        dead = false;

        yield break;
    }
    #endregion
}
