using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class Rules : MonoBehaviour
{
    public static GameObject photoButton;

    public bool isScannable;
    public bool hasTimer;
    public bool canTakePhoto;

    public float elapsedTime = 300.0f;

    protected GameObject timerObject;
    protected GameObject scanner;

    private TimeSpan timePlaying;

    private void Awake()
    {
        timerObject = GameObject.Find("Timer");
        scanner = FindObjectOfType<ActionScanner>().GetComponentInChildren<RawImage>().gameObject;
        photoButton = FindObjectOfType<Button>().gameObject;
    }

    protected void SetRules()
    {
        scanner.SetActive(isScannable);
        timerObject.SetActive(hasTimer);
        photoButton.SetActive(canTakePhoto);
    }

    protected void Timer()
    {
        elapsedTime -= Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timeText = timePlaying.ToString("m':'ss'.'ff");
        timerObject.GetComponent<TextMeshProUGUI>().text = timeText;
    }

    protected void CheckOffQuest()
    {
        PlayerPrefs.SetString("questID", PlayerPrefs.GetString("modelID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
