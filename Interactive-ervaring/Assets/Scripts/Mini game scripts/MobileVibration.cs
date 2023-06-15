using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileVibration : MonoBehaviour
{
    public GameObject finalReward;
    public GameObject tutorialcharacter;
    public Rules rules = new Rules();
    private Tutorial tutorial;
    private Gyroscope gyro;

    private Animator animator;

    private int lockPickValue;
    private int rotateValue;
    private int differenceInValue;

    private bool gyroEnabled;
    private bool canGetNewValue = true;
    private bool done = false;
    private bool foundPiece;

    private float vibrationDuration = 1.0f;
    private float coolDownDuration = 3.0f;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        rules.SetRules();

        gyroEnabled = EnableGyro();
        animator = FindObjectOfType<Animator>();
        GameObject.Instantiate(tutorialcharacter, tutorialcharacter.transform);
        tutorial = tutorialcharacter.GetComponent<Tutorial>();
        lockPickValue = Random.Range(-100, 100);
    }

    void Update()
    {
        if (!rules.StartGame())
        {
            return;
        }

        if (!Debugger.OnDevice())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                done = true;
            }
        }

        FoundPiece();

        if (done)
        {
            // play animation
            animator.SetBool("openChest", true);
            return;
        }

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

        differenceInValue = Mathf.Abs(lockPickValue - rotateValue);
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

    public void OpenChest()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        Inventory.GetInstance().amountOfCrownPieces++;

        if(Inventory.GetInstance().amountOfCrownPieces == 4)
        {
            rules.SetPicture(true);
            rules.rewardObject = finalReward;
        }

        rules.ShowReward(FindObjectOfType<ObjectSpawner>().transform);
        foundPiece = true;
    }

    private void FoundPiece()
    {
        if (!foundPiece)
        {
            return;
        }

        if (rules.photoCapture.tookPhoto && rules.photoCapture.gameObject.activeSelf)
        {
            rules.CheckOffQuest();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                tutorial.LastLine();
            }
        }
    }

    private void VibrationTimer()
    {
        if (canGetNewValue)
        {
            int y = differenceInValue / 10;
            float duration = (5 - y) * 0.5f;

            vibrationDuration = duration;

            canGetNewValue = false;
        }

        if(differenceInValue > 40)
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
