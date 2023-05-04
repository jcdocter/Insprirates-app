using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Rules : MonoBehaviour
{
    public bool isScannable;
    public bool hasTimer;

    public float elapsedTime = 300.0f;

    protected string questScene;
    protected GameObject timerObject;
    protected GameObject scanner;

    private TimeSpan timePlaying;

    protected void Awake()
    {
        timerObject = FindObjectOfType<ActionScanner>().GetComponentInChildren<TextMeshProUGUI>().gameObject;
        scanner = FindObjectOfType<ActionScanner>().gameObject;
    }

    protected void SetRules()
    {
        scanner.SetActive(isScannable);
        timerObject.SetActive(hasTimer);

        questScene = "QuestPage";
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
        PlayerPrefs.SetString("buttonID", ObjectSpawner.questID);
        SceneManager.LoadScene(questScene);
    }
}
