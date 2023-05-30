using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public bool isTest;
    public float resetTimer;

    public GameObject[] fishObjects;
    public Vector2 limitRange;
    public Vector3 startPosition;

    public Rules rules = new Rules();

    private Gyroscope gyro;

    private bool hasThrown;
    private bool gyroEnabled;

    private float throwSpeed = 4.0f;
    private float height;
    private float releasePower;
    private float startTimer;

    private void Start()
    {
        rules.SetRules();

        startTimer = resetTimer;
        gyroEnabled = EnableGyro();
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

        Throw();
    }

    private void Throw()
    {
        if (!Debugger.OnDevice())
        {
            CalculateThrow();
            return;
        }

        if (gyroEnabled)
        {
            if (transform.position.z >= 9.0f)
            {
                resetTimer -= Time.deltaTime;

                if(resetTimer <= 0.0f)
                {
                    resetTimer = startTimer;
                    transform.position = startPosition;
                    hasThrown = false;
                }

                return;
            }

            if (Input.GetMouseButton(0) && !hasThrown)
            {
                if (Input.acceleration.y > 0)
                {
                    releasePower = Input.acceleration.y;
                    hasThrown = true;
                }
            }

            if (hasThrown)
            {
                Debugger.WriteData(releasePower.ToString());
                transform.position += new Vector3(0.0f, releasePower * throwSpeed * Time.deltaTime, throwSpeed * Time.deltaTime);
            }
            else
            {
                if(Input.GetMouseButton(0))
                {
                    return;
                }

                releasePower = 0;

                Vector3 screen = transform.position;
                screen.x = Mathf.Clamp(transform.position.x, limitRange.x, limitRange.y);

                transform.position = screen;
                transform.Translate(Vector3.right * Time.deltaTime * 1.0f * gyro.rotationRateUnbiased.y);
            }
        }
    }

    private void CalculateThrow()
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

        if (!hasThrown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 screen = transform.position;
            screen.x = Mathf.Clamp(transform.position.x, limitRange.x, limitRange.y);

            transform.position = screen;
            transform.Translate(Vector3.right * Time.deltaTime * 1.0f * horizontalInput);

            return;
        }

        transform.position += new Vector3(0.0f, height * Time.deltaTime, throwSpeed * Time.deltaTime);

        if (transform.position.z >= 9.0f)
        {
            hasThrown = false;
        }
    }

    private void SpawnFish()
    {
       foreach (GameObject fish in fishObjects) 
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
            Quaternion rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            Instantiate(fish, spawnPoint, rotation);
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            return true;
        }

        return false;
    }
}

