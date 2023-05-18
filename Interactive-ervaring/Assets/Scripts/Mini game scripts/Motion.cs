using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    private float xRotation;
    private float yRotation;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            float moveX = gyro.rotationRateUnbiased.x * Time.deltaTime * sensitivityX;
            float moveY = gyro.rotationRateUnbiased.y * Time.deltaTime * sensitivityY;

            yRotation += moveX;
            xRotation -= moveY;

            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        }

    }

    private bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            return true;
        }

        return false;
    }
}
