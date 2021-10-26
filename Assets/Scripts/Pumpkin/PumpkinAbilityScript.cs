using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinAbilityScript : MonoBehaviour
{

    public float rayRadius;
    public float destroyDelay;

    public void Explode()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.CircleCastAll(transform.position, rayRadius, Vector2.up);
        if (hit.Length > 0)
        {
            foreach(RaycastHit2D i in hit)
            {
                print(i.collider.gameObject.name);
                Destroy(i.collider.gameObject);
            }
        }
    }
}
