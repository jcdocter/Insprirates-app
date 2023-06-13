using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [HideInInspector]
    public bool hideObject = false;

    public bool isTest;
    public float resetTimer;

    public GameObject[] fishObjects;
    public Vector2 limitRange;
    public Vector3 startPosition;

    public Rules rules = new Rules();

    private List<GameObject> fishList = new List<GameObject>();
    private Swipe swipe = new Swipe();

    private bool hasThrown;

    private float throwSpeed = 4.0f;
    private float height;
    private float releasePower;
    private float startTimer;

    private void Start()
    {
        rules.SetRules();

        startTimer = resetTimer;
        SpawnFish();

        if(!isTest)
        {
            transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        transform.localScale = new Vector3(300, 300, 300);
        transform.position = startPosition;
    }

    private void Update()
    {
        if(!rules.StartGame())
        {
            return;
        }

        if(hideObject)
        {
            RemoveFish();
            this.gameObject.SetActive(false);
        }

        Throw();
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
            transform.position += new Vector3(0.0f, releasePower * throwSpeed * Time.deltaTime, throwSpeed * Time.deltaTime);
            return;
        }

        if (Input.acceleration.y > 0 || swipe.CheckSwipe())
        {
            releasePower = Input.acceleration.y > 0 ? Mathf.Clamp(Input.acceleration.y, -5.0f, 5.0f) : Mathf.Clamp(swipe.endTouchPosition.y - swipe.startTouchPosition.y, -5.0f, 5.0f);

            hasThrown = true;
        }
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

    private void RemoveFish()
    {
        foreach(GameObject fish in fishList)
        {
            fish.SetActive(false);
        }
    }
}

