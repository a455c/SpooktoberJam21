using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinGuide : MonoBehaviour
{
    public Vector2 guideDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PumpkinMovement pumpkinMovement = collision.gameObject.GetComponent<PumpkinMovement>();
            pumpkinMovement.direction = guideDirection;
        }
    }
}
