using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Fishing : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private TextMeshProUGUI debugDataText;
    private Vector3 rotation;

    private void Start()
    {
        gyroEnabled = EnableGyro();
        debugDataText = FindObjectOfType<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            rotation = new Vector3(-gyro.rotationRateUnbiased.x, gyro.rotationRateUnbiased.y, 0.0f);
            debugDataText.text = rotation.ToString();
           // transform.Rotate(rotation);
            transform.localRotation *= Quaternion.Euler(rotation);
          //  transform.localEulerAngles = rotation;
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
