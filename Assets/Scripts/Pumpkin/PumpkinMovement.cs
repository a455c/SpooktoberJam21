using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMovement : MonoBehaviour
{
    public Transform targetPos; // position pumpkin will move towards
    public Vector3 direction; // direction rthe current pumpkin is going


    public GameObject PumpkinGuidePrefab;

    public float moveSpeed; // how fast pumpkin will reach the target position

    public bool isSelected = false; // if the current pumpkin has been pressed
    public bool choosingAbility = false;

    float lastDelay; // the last marked delay of the pumpkins movement
    public float delayTime; // how quickly the target position will move to the next destination

    public int waitforinputDelay; // how long to wait for a selected pumpkins input

    public float rayDist; // the distance of the raycast for the collsion of pumpkins

    bool wPressed = false;


    void Start()
    {
        // setting variables
        direction = new Vector2(1, 0);
        targetPos.parent = null;
        lastDelay = Time.time;
    }

    void Update()
    {
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

        // when will the target position move towards the next increment
        if (Vector3.Distance(transform.position, targetPos.position) <= .05f && Time.time >= lastDelay + delayTime && !isSelected)
        {
            // how far the next increment is eg: 1 jump
            targetPos.position += direction;
            lastDelay = Time.time;
        }


        LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDist, obstacleLayer); // casting a raycast to detect obstacles

        if (hit.collider != null)
        {
            direction = -direction; // sets the pumpkins direction to opposite
        }
    }
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSelected)
                isSelected = true; // the gameobject has been selected
            else
                isSelected = false; // the gameobject has been deselected
        }
    }

    IEnumerator WaitForInput(bool bl)
    {
        yield return new WaitForSeconds(waitforinputDelay); // waits for a certain amount of time
        bl = false;
    }

    private void AbilityInput()
    {

        GuideAbilityInput();   
        
        //else if (Input.GetKeyDown(KeyCode.A))
        {
            // decoy pumkin
        }
       // else if (Input.GetKeyDown(KeyCode.S))
        {
            // ghost pumkin
        }
       // else if (Input.GetKeyDown(KeyCode.D))
        {
            // guide pumkin
        }
    }

    private void GuideAbilityInput()
    {
        if (Input.GetKeyUp(KeyCode.W))
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
        }
    }

    
}
