using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileVibration : MonoBehaviour
{
    [HideInInspector]
    public GameObject tutorialClone;

    public GameObject tutorialCharacter;

    public GameObject finalReward;
    public Rules rules = new Rules();
    private Gyroscope gyro;
    private Vector3 originalScale;

    private Animator animator;

    private int lockPickValue;
    private int rotateValue;
    private int differenceInValue;
    private int questID;

    private bool gyroEnabled;
    private bool canGetNewValue = true;
    private bool canStart = true;
    private bool done = false;
    private bool foundPiece;

    private float vibrationDuration = 1.0f;
    private float coolDownDuration = 3.0f;

    private void Start()
    {
        rules.SetPicture(false);
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        tutorialClone = GameObject.Instantiate(tutorialCharacter);

        if (Inventory.GetInstance().amountOfCrownPieces > 0 || Debugger.OnDevice())
        {
            tutorialClone.SetActive(false);
        }
    }

    private void Update()
    {
        if (canStart && !tutorialClone.activeSelf)
        {
            SetTreasureGame();
            canStart = false;
        }

        if (tutorialClone.activeSelf)
        {
            return;
        }

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

            differenceInValue = Mathf.Abs(lockPickValue - rotateValue);
            VibrationTimer();
        }

    }

    private void SetTreasureGame()
    {
        rules.SetRules();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = originalScale;
        gyroEnabled = EnableGyro();
        animator = FindObjectOfType<Animator>();
        lockPickValue = Random.Range(-100, 100);
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
        Screen.orientation = ScreenOrientation.Portrait;
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        Inventory.GetInstance().amountOfCrownPieces++;

        if(Inventory.GetInstance().amountOfCrownPieces == 4)
        {
            questID = PlayerPrefs.GetInt("questID");
            PlayerPrefs.SetInt("questID", 7);
            rules.SetPicture(true);
            rules.rewardObject = finalReward;
        }

        rules.ShowReward(new Vector3(0,0, 5.0f));
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
            PlayerPrefs.SetInt("questID", questID);
            rules.CheckOffQuest();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(Inventory.GetInstance().amountOfCrownPieces == 1)
                {
                    rules.rewardObject.SetActive(false);
                    tutorialClone.SetActive(true);
                    FindObjectOfType<Tutorial>().LastLine();
                }
                else
                {
                    rules.CheckOffQuest();
                }

            }
        }
    }

    private void VibrationTimer()
    {
   //     Debugger.WriteData($"{rotateValue} == {lockPickValue}");
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
