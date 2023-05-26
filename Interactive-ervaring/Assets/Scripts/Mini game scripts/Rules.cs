using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    public GameObject photoButton;
    public GameObject pauseObject;

    public bool canTakePhoto;

    public float elapsedTime = 300.0f;

    private Transform canvasTransform;
    private GameObject pauseScreen;
    private bool canStartGame = false;

    public void SetRules()
    {
        canvasTransform = GameObject.FindObjectOfType<Canvas>().transform;

        pauseScreen = GameObject.Instantiate(pauseObject, pauseObject.transform);

        if(canTakePhoto)
        {
            GameObject camera = GameObject.Instantiate(photoButton, canvasTransform);
            camera.transform.parent = canvasTransform;
        }
    }

    public bool StartGame()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject.Destroy(pauseScreen);
            canStartGame = true;
        }

        return canStartGame;
    }

    public void CheckOffQuest()
    {
        PlayerPrefs.SetInt("confirmedID", PlayerPrefs.GetInt("questID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
