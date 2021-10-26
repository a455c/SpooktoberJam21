using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject PumpkinBasePrefab;
    public GameObject SpawningPumpkinPrefab;

    public PumpkinCounterScript pumpkinCounter;

    public Transform SpawnTargetPos;
    public Transform SpawnPos;

    public float timeDelay;
    float lastDelay;

    GameObject clone;
    public float moveSpeed;

    bool isCloned = false;

    // Start is called before the first frame update
    void Start()
    {
        lastDelay = Time.time - timeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCloned)
        {
            clone.transform.position = Vector2.MoveTowards(clone.transform.position, SpawnTargetPos.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(clone.transform.position, SpawnTargetPos.position) <= 0.5f)
            {
                GameObject pumpkinClone = Instantiate(PumpkinBasePrefab, clone.transform.position, Quaternion.identity);
                pumpkinClone.SetActive(true);
                Destroy(clone);
                isCloned = false;
            }
        }
              
        if (Time.time >= lastDelay + timeDelay && !pumpkinCounter.isDead)
        {
            clone = Instantiate(SpawningPumpkinPrefab, SpawnPos.position, Quaternion.identity);
            clone.SetActive(true);
            isCloned = true;
            lastDelay = Time.time;
          
        }
    }
}
