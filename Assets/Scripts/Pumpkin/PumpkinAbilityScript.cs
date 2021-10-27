using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinAbilityScript : MonoBehaviour
{

    public float rayRadius;
    public float destroyDelay;

    public bool isExploded = false;

    public void Explode()
    {
        LayerMask destroyableLayer = LayerMask.GetMask("Destroyable");

        RaycastHit2D[] hit;
        hit = Physics2D.CircleCastAll(transform.position, rayRadius, Vector2.up, 3f, destroyableLayer);
        if (hit.Length > 0)
        {
            foreach(RaycastHit2D i in hit)
            {
                print(i.collider.gameObject.name);
                Destroy(i.collider.gameObject);
            }
        }

        isExploded = true;
    }
}


