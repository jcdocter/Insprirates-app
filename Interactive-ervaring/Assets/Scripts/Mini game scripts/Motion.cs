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
    private Vector3 rotation;

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


            // put this in other code
/*            rotation = new Vector3(gyro.rotationRateUnbiased.x, gyro.rotationRateUnbiased.y, 0.0f);
            transform.localEulerAngles -= rotation;
            Debugger.WriteData(transform.localEulerAngles.ToString());*/
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
