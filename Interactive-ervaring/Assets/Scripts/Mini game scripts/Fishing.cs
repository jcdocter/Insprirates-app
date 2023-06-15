using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [HideInInspector]
    public bool hideObject = false;

    public float resetTimer;

    [HideInInspector]
    public GameObject tutorialClone;

    public GameObject tutorialCharacter;

    public GameObject[] fishObjects;
    public Vector3 startPosition;

    public Rules rules = new Rules();

    private List<GameObject> fishList = new List<GameObject>();
    private Swipe swipe = new Swipe();


    private bool hasThrown;
    private bool canStart = true;

    private float throwSpeed = 4.0f;
    private float height;
    private float releasePower;
    private float startTimer;

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
        rules.SetPicture(false);
        tutorialClone = GameObject.Instantiate(tutorialCharacter);

        if (Inventory.GetInstance().amountOfFish > 0)
        {
            tutorialClone.SetActive(false);
        }
    }

    private void Start()
    {
        if(!tutorialClone.activeSelf)
        {
            SetFishGame();
            canStart = false;
        }
    }

    private void Update()
    {
        if(canStart && !tutorialClone.activeSelf)
        {
            SetFishGame();
            canStart = false;
        }

        if(tutorialClone.activeSelf)
        {
            return;
        }

        if(!rules.StartGame())
        {
            return;
        }

        Throw();
    }

    private void SetFishGame()
    {
        rules.SetRules();

        startTimer = resetTimer;
        SpawnFish();

        transform.localScale = new Vector3(300, 300, 300);
        transform.position = startPosition;
    }

    private void Throw()
    {
        if (!Debugger.OnDevice())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasThrown = true;
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                hasThrown = false;
                transform.position = startPosition;
            }

            if (hasThrown)
            {
                transform.position += new Vector3(0.0f, height * Time.deltaTime, throwSpeed * Time.deltaTime);

                if (transform.position.z >= 9.0f)
                {
                    hasThrown = false;
                }
            }

            return;
        }

        CalculateThrow();
    }

    private void CalculateThrow()
    {
        if (transform.position.z >= 9.0f)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0.0f)
            {
                resetTimer = startTimer;
                transform.position = startPosition;
                hasThrown = false;
            }

            return;
        }

        if (hasThrown)
        {
            float yDirection = (releasePower/10) * throwSpeed * Time.deltaTime;

            if (transform.position.y > releasePower)
            {
                yDirection = releasePower;
            }

            if(transform.position.y < -releasePower)
            {
                yDirection = -releasePower;
            }

            transform.position += new Vector3(0.0f, yDirection, throwSpeed * Time.deltaTime);
            Debugger.WriteData($"{transform.position.y}");
            return;
        }

        if (Input.acceleration.y > 0 || swipe.CheckSwipe())
        {
            releasePower = Input.acceleration.y > 0 ? Scale(0.0f, 1.0f, -5.0f, 5.0f, Input.acceleration.y) : Scale(42.0f, 2414.0f, -5.0f, 5.0f, swipe.endTouchPosition.y - swipe.startTouchPosition.y);

           hasThrown = true;
        }
    }

    private float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    private void SpawnFish()
    {
       foreach (GameObject fish in fishObjects) 
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
            GameObject fishObject = Instantiate(fish, spawnPoint, Quaternion.identity);

            fishList.Add(fishObject);
        }
    }

    public void RemoveFish()
    {
        foreach(GameObject fish in fishList)
        {   
            fish.SetActive(false);
        }

        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}

