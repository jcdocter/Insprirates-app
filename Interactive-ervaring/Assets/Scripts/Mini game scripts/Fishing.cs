using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameObject[] fishObjects;
    private Gyroscope gyro;
    private Vector3 startPosition;

    private bool hasThrown;
    private bool gyroEnabled;

    private float throwSpeed = 4.0f;
    private float height;
    private float releasePower;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        SpawnFish();

        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.37f, Camera.main.transform.position.z + 0.6f);

        transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        transform.localScale = new Vector3 (300, 300, 300);
        startPosition = transform.position;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Throw();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hasThrown = false;
                transform.position = startPosition;
            }
        }

        if(!Debugger.OnDevice())
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

            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 screen = transform.position;
            screen.x = Mathf.Clamp(transform.position.x, -0.15f, 0.15f);

            transform.position = screen;
            transform.Translate(Vector3.right * Time.deltaTime * 1.0f * horizontalInput);

            if (!hasThrown)
            {
                return;
            }

            transform.position += new Vector3(0.0f, height * Time.deltaTime, throwSpeed * Time.deltaTime);

            if (transform.position.z >= 9.0f)
            {
                hasThrown = false;
            }
        }
    }

    private void Throw()
    {
        if (transform.position.z >= 9.0f)
        {
            return;
        }

        if(Input.GetMouseButton(0) && !hasThrown)
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
            releasePower = 0;

            Vector3 screen = transform.position;
            screen.x = Mathf.Clamp(transform.position.x, -0.15f, 0.15f);

            transform.position = screen;
            transform.Translate(Vector3.right * Time.deltaTime * 1.0f * gyro.rotationRateUnbiased.y);
        }
    }

    private void SpawnFish()
    {
       foreach (GameObject fish in fishObjects) 
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
            Instantiate(fish, spawnPoint, Quaternion.identity);
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

