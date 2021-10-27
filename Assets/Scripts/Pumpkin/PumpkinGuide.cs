using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinGuide : MonoBehaviour
{
    public Vector2 guideDirection;

    public GuidePumpkinParticles guideParticles;
    bool particlePlayed = false;


    private void Update()
    {
        if (!particlePlayed)
        {
            guideParticles.guideAbilityParticle.Play();
            particlePlayed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // setting the pupkins to the new direction
            PumpkinMovement pumpkinMovement = collision.gameObject.GetComponent<PumpkinMovement>();
            pumpkinMovement.direction = guideDirection;

            if (!guideParticles.guideActiveParticle.isPlaying)
            {
                guideParticles.guideActiveParticle.Play();
            }
        }
       
    }
}
