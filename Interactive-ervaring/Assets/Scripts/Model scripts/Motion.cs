using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private TextMeshProUGUI debugDataText;
    private Vector3 rotation;
    private Vector3 position;

    private float fallSpeed = 0.6f;
    private Vector2 acceleration;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        debugDataText = FindObjectOfType<TextMeshProUGUI>();
        position = transform.position;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            // put this in other code
            //  rotation = new Vector3(-gyro.rotationRateUnbiased.x, gyro.rotationRateUnbiased.y, 0.0f);
            //  transform.localEulerAngles += rotation;
            //  transform.localEulerAngles = rotation;

            if(transform.position.y <= -1.6f)
            {
                position.y = -1.6f;
                transform.position = position;
                transform.position += new Vector3(acceleration.x, 0.0f, 0.0f);

                if (Input.acceleration.y >= 0.0f)
                {
                    acceleration += new Vector2(0.0f, Input.acceleration.y);

                    debugDataText.text = acceleration.y.ToString();
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

    private bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

       //     rotation = new Quaternion(0.0f, 1.0f, 0.0f,0.0f);

            return true;
        }

        return false;
    }
}
