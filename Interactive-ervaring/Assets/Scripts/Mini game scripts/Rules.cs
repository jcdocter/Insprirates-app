using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    public GameObject photoButton;

    public bool canTakePhoto;

    public float elapsedTime = 300.0f;

    private Transform canvasTransform;

    public void SetRules()
    {
        canvasTransform = GameObject.FindObjectOfType<Canvas>().transform;

        if(canTakePhoto)
        {
            GameObject camera = GameObject.Instantiate(photoButton, canvasTransform);
            camera.transform.parent = canvasTransform;
        }
    }

    public void CheckOffQuest()
    {
        PlayerPrefs.SetInt("confirmedID", PlayerPrefs.GetInt("questID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
