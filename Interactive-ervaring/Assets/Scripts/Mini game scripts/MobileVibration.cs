using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileVibration : MonoBehaviour
{
    public Rules rules = new Rules();
    private Gyroscope gyro;

    private int lockPickValue;
    private int rotateValue;

    private bool gyroEnabled;
    private bool canGetNewValue = true;

    private float vibrationDuration = 5.0f;
    private float coolDownDuration = 3.0f;

    //debug variable. Can be deleted later
    private bool done = false;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        rules.SetRules();
        gyroEnabled = EnableGyro();
        lockPickValue = Random.Range(-100, 100);
    }

    void Update()
    {
        if (!rules.StartGame())
        {
            return;
        }

        if (done)
        {
            Inventory.GetInstance().amountOfRecruits++;
            rules.CheckOffQuest();
            return;
        }
        Debugger.WriteData($"{lockPickValue} == {rotateValue}");

        if (gyroEnabled)
        {
            if(gyro.rotationRateUnbiased.z > 0.1f)
            {
                rotateValue++;
            }
            else if(gyro.rotationRateUnbiased.z < -0.1f)
            {
                rotateValue--;
            }
        }

        VibrationTimer();
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

    private void VibrationTimer()
    {
        int DifferenceInValue = Mathf.Abs(lockPickValue - rotateValue);
        Debugger.WriteData($"{lockPickValue} == {rotateValue}");

        if(canGetNewValue)
        {
            if(DifferenceInValue < 40)
            {
                vibrationDuration = 1.0f;
            }

            if (DifferenceInValue < 30)
            {
                vibrationDuration = 1.5f;
            }

            if (DifferenceInValue < 20)
            {
                vibrationDuration = 2.0f;
            }

            if (DifferenceInValue < 10)
            {
                vibrationDuration = 2.5f;
            }

            canGetNewValue = false;
        }

        if(DifferenceInValue > 40)
        {
            coolDownDuration = 3.0f;
            canGetNewValue = true;

            return;
        }

        while (vibrationDuration > 0.0f)
        {
            vibrationDuration -= Time.deltaTime;
            Handheld.Vibrate();

            return;
        }

        if(vibrationDuration <= 0.0f)
        {
            if (lockPickValue == rotateValue)
            {
                done = true;
            }
        }

        while (coolDownDuration > 0.0f)
        {
            coolDownDuration -= Time.deltaTime;
            return;
        }

        if(coolDownDuration <= 0.0f)
        {
            coolDownDuration = 3.0f;
            canGetNewValue = true;
        }
    }
}
