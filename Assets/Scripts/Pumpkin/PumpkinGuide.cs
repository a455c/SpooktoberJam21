using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinGuide : MonoBehaviour
{
    public Vector2 guideDirection;

    public GuidePumpkinParticles guideParticles;
    bool particlePlayed = false;

    public GameObject spriteGameobject;

    private void Update()
    {
        if (!particlePlayed)
        {
            guideParticles.guideAbilityParticle.Play();
            particlePlayed = true;
        }
       
        transform.up = new Vector3(transform.position.x + guideDirection.x, transform.position.y + guideDirection.y) - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // setting the pupkins to the new direction
            PumpkinMovement pumpkinMovement;
            pumpkinMovement = collision.gameObject.GetComponent<PumpkinMovement>();
            IEnumerator WaitToGuide()
            {
                yield return new WaitForSeconds(0.5f);
                pumpkinMovement.direction = guideDirection;
            }
            StartCoroutine(WaitToGuide());

            if (!guideParticles.guideActiveParticle.isPlaying)
            {
                guideParticles.guideActiveParticle.Play();
            }
        }
       
    }

    
}
