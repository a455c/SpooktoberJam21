using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMovement : MonoBehaviour
{
    public Transform targetPos; // position pumpkin will move towards
    public Vector3 direction; // direction the current pumpkin is going
    public PumpkinCounterScript pumpkinCounter; // reference to the pumpkin counter script

    public GameObject PumpkinGuidePrefab; // reference to the prefab so we can clone the gameobject
    public GameObject PumpkinExplodePrefab;

    public float moveSpeed; // how fast pumpkin will reach the target position

    public bool isSelected = false; // if the current pumpkin has been pressed
    public bool choosingAbility = false;

    float lastDelay; // the last marked delay of the pumpkins movement
    public float delayTime; // how quickly the target position will move to the next destination

    public float rayDist; // the distance of the raycast for the collsion of pumpkins

    bool wPressed = false;

    bool rightRotated = true;
    bool leftRotated = false;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        // setting variables
        direction = new Vector2(1, 0);
        targetPos.parent = null;
        lastDelay = Time.time;
        animator.SetBool("moving_right", true);
    }

    void Update()
    {
        // playing functions
        PumpkinMove();

        if (isSelected)
        {
            AbilityInput();
        }
    }

    private void PumpkinMove()
    {
        // the pumpkin will make its way towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);

        if (direction == new Vector3(1, 0))
        {
            animator.SetBool("moving_right", true);
            animator.SetBool("moving_up", false);
            animator.SetBool("moving_down", false);
            spriteRenderer.flipX = false;
        }
        else if (direction == new Vector3(-1, 0))
        {
            animator.SetBool("moving_right", true);
            animator.SetBool("moving_up", false);
            animator.SetBool("moving_down", false);
            spriteRenderer.flipX = true;
        }
        else if (direction == new Vector3(0,1))
        {
            animator.SetBool("moving_right", false);
            animator.SetBool("moving_up", true);
            animator.SetBool("moving_down", false);
        }
        else if (direction == new Vector3(0,-1))
        {
            animator.SetBool("moving_right", false);
            animator.SetBool("moving_up", false);
            animator.SetBool("moving_down", true);
        }

        // when will the target position move towards the next increment
        if (Vector3.Distance(transform.position, targetPos.position) <= .05f && Time.time >= lastDelay + delayTime && !isSelected)
        {
            // how far the next increment is eg: 1 jump
            targetPos.position += direction;
            lastDelay = Time.time;
        }
        // layermask to only raycast to
        LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
        LayerMask destroyableLayer = LayerMask.GetMask("Destroyable");

        // raycasting
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDist, obstacleLayer); // casting a raycast to detect obstacles
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, direction, rayDist, destroyableLayer);

        // does the raycast hit
        if (hit.collider != null)
        {
            direction = -direction; // sets the pumpkins direction to opposite
        }
        if (hit1.collider != null)
        {
            direction = -direction; // sets the pumpkins direction to opposite
        }


        if (pumpkinCounter.isDead)
        {
            Destroy(gameObject);
            Destroy(targetPos.gameObject);
        }
    }

    private void OnMouseOver()
    {
        // left click
        if (Input.GetMouseButtonDown(0) && !pumpkinCounter.onePumpkinSelected)
        {
            if (!isSelected)
            {
                isSelected = true; // the gameobject has been selected
                pumpkinCounter.onePumpkinSelected = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && isSelected)
        {
            isSelected = false;
            pumpkinCounter.onePumpkinSelected = false;
        }
    }

    private void AbilityInput()
    {
        // guide pumpkin
        GuideAbilityInput();

        // explode pumpkin
        if (Input.GetKeyUp(KeyCode.A) && !pumpkinCounter.maxExplode)
        {
            CreateAbilityPumpkin("explode", PumpkinExplodePrefab);
            StartCoroutine(WaitToExplode(gameObject));
            StartCoroutine(WaitToExplode(targetPos.gameObject));
            pumpkinCounter.maxExplodeAbility -= 1;
        }
    }

    private void CreateAbilityPumpkin(string str, GameObject prefab)
    {
        if (transform.position == targetPos.position && !wPressed)
        {
            GameObject pumpkinAbility = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            pumpkinAbility.SetActive(true);
            PumpkinAbilityScript pumpkinAbilityScript = pumpkinAbility.GetComponent<PumpkinAbilityScript>();

            if (str == "explode")
                pumpkinAbilityScript.Explode();


            Destroy(gameObject);
            Destroy(targetPos.gameObject);
            pumpkinCounter.onePumpkinSelected = false;
            isSelected = false;
        }
    }
    private void GuideAbilityInput()
    {
        if (Input.GetKeyUp(KeyCode.W) && !pumpkinCounter.maxGuide)
        {
            wPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && wPressed)
        {
            // up
            CreatePumpkinGuide(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.A) && wPressed)
        {
            // left
            CreatePumpkinGuide(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.S) && wPressed)
        {
            // down
            CreatePumpkinGuide(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.D) && wPressed)
        {
            // right
            CreatePumpkinGuide(new Vector2(1, 0));
        }
    }
    private void CreatePumpkinGuide(Vector2 dir)
    {
        if (transform.position == targetPos.position)
        {
            GameObject pumpkinGuide = Instantiate(PumpkinGuidePrefab, gameObject.transform.position, Quaternion.identity);
            pumpkinGuide.SetActive(true);
            PumpkinGuide pumpkinGuideScript = pumpkinGuide.GetComponent<PumpkinGuide>();
            pumpkinGuideScript.guideDirection = dir;


            Destroy(gameObject);
            Destroy(targetPos.gameObject);
            wPressed = false;
            isSelected = false;
            pumpkinCounter.onePumpkinSelected = false;
            pumpkinCounter.maxGuideAbility -= 1;
        }
    }

    private IEnumerator WaitToExplode(GameObject gmj)
    {
        yield return new WaitForSeconds(1);
        Destroy(gmj);
    }
}
