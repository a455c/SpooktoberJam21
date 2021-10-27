using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePumpkinParticle : MonoBehaviour
{
    public PumpkinAbilityScript pumpkinAbility;


    public ParticleSystem explosionParticle;

    // Update is called once per frame
    void Update()
    {
        if (pumpkinAbility.isExploded)
        {
            if (!explosionParticle.isPlaying)
            {
                explosionParticle.Play();
                pumpkinAbility.isExploded = false;
            }
        } 
    }
}
