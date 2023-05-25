using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[Serializable]
public class Rules
{
    public GameObject photoButton;
    public GameObject timerObject;

    public bool hasTimer;
    public bool canTakePhoto;

    public float elapsedTime = 300.0f;

    private TimeSpan timePlaying;
    private Transform canvasTransform;

    public void SetRules()
    {
        canvasTransform = GameObject.FindObjectOfType<Canvas>().transform;

        if(hasTimer)
        {
            GameObject timer = GameObject.Instantiate(timerObject, canvasTransform);   
            timer.transform.parent = canvasTransform;
        }

        if(canTakePhoto)
        {
            GameObject camera = GameObject.Instantiate(photoButton, canvasTransform);
            camera.transform.parent = canvasTransform;
        }
    }

    public void Timer()
    {
        elapsedTime -= Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timeText = timePlaying.ToString("m':'ss'.'ff");
        timerObject.GetComponent<TextMeshProUGUI>().text = timeText;
    }

    public void CheckOffQuest()
    {
        PlayerPrefs.SetString("questID", PlayerPrefs.GetString("modelID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
