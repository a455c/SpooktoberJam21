using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PumpkinCounterScript : MonoBehaviour
{
    public float maxExplodeAbility;
    public float maxGuideAbility;

    public bool maxExplode = false;
    public bool maxGuide = false;

    public float timerDeathDelay;
    private float lastTime;
    public float currentTime;
    private float time;

    public bool isDead = false;

    public float maxCompleted;
    public float currentCompleted;

    public bool onePumpkinSelected = false;

    public LevelLoader levelLoader;

    public AudioSource reachedHideaway;
    public AudioSource endLevel;
    public AudioSource gameOver;

    public bool endLevelSfxPlayed = false;
    public bool gameOverSfxPlayed = false;

    private void Start()
    {
        Time.timeScale = 1;
        time = Time.timeSinceLevelLoad;
        lastTime = time;
        timerDeathDelay = 60;
    }

    void Update()
    {
        time = Time.timeSinceLevelLoad;


        if (maxGuideAbility < 1)
        {
            maxGuide = true;
        }
        if (maxExplodeAbility < 1)
        {
            maxExplode = true;
        }

        if (time >= lastTime + timerDeathDelay)
        {
            if (!gameOver.isPlaying)
            {
                if (!gameOverSfxPlayed)
                {
                    gameOver.Play();
                    gameOverSfxPlayed = true;
                }
            }
            isDead = true;
            levelLoader.LoadScene(0, "End");
        }

        if(!isDead)
            currentTime = Mathf.Abs(System.Convert.ToInt64(time - timerDeathDelay));

        if (SceneManager.GetActiveScene().buildIndex != 6)
        {
            if (maxCompleted <= currentCompleted)
            {
                if (!endLevel.isPlaying)
                {
                    if (!endLevelSfxPlayed)
                    {
                        endLevel.Play();
                        endLevelSfxPlayed = true;
                    }
                }
                levelLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, "End");

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currentCompleted += 1;
            PumpkinMovement pumMove = collision.gameObject.GetComponent<PumpkinMovement>();
            Destroy(pumMove.targetPos.gameObject);
            Destroy(collision.gameObject);
            reachedHideaway.Play();
        }
    }
}
