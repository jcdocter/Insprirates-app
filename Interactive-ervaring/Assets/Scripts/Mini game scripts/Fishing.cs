using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public GameObject[] fishObjects;

    private Gyroscope gyro;
    private Vector2 acceleration;
    private Vector3 position;

    private bool gyroEnabled;
    private float fallSpeed = 0.6f;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        SpawnFish();
        position = transform.position;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            if(transform.position.y <= -1.6f)
            {
                position.y = -1.6f;
                transform.position = position;
                transform.position += new Vector3(acceleration.x, 0.0f, 0.0f);

                if (Input.acceleration.y >= 0.0f)
                {
                    acceleration += new Vector2(0.0f, Input.acceleration.y);
                    transform.position += new Vector3(0.0f, acceleration.y, 0.0f);
                }
            }
            else
            {
                transform.position -= new Vector3(0.0f, fallSpeed * Time.deltaTime, 0.0f);
            }

            acceleration = new Vector2(gyro.attitude.y, Input.acceleration.y);
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

