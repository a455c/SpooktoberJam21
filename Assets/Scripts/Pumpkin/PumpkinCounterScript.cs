using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinCounterScript : MonoBehaviour
{
    public float maxExplodeAbility;
    public float maxGuideAbility;

    public bool maxExplode = false;
    public bool maxGuide = false;

    public float timerDeathDelay;
    private float lastTime;
    public float currentTime;

    public bool isDead = false;

    public float maxCompleted;
    public float currentCompleted;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if(maxGuideAbility < 1)
        {
            maxGuide = true;
        }
        if (maxExplodeAbility < 1)
        {
            maxExplode = true;
        }

        if (Time.time >= lastTime + timerDeathDelay)
        {
            isDead = true;
        }

        if(!isDead)
            currentTime = Mathf.Abs(System.Convert.ToInt64(Time.time - timerDeathDelay));

        if (maxCompleted <= currentCompleted)
            print("completed");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currentCompleted += 1;
            PumpkinMovement pumMove = collision.gameObject.GetComponent<PumpkinMovement>();
            Destroy(pumMove.targetPos.gameObject);
            Destroy(collision.gameObject);      
        }
    }
}
