using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    [HideInInspector]
    public Canvas pauseScreen;
    
    public GameObject photoButton;
    public Canvas pauseObject;

    public bool canTakePhoto;

    public float elapsedTime = 300.0f;

    private Transform canvasTransform;
    private bool canStartGame = false;

    public void SetRules()
    {
        canvasTransform = GameObject.FindObjectOfType<RecCamera>().transform;

        pauseScreen = GameObject.Instantiate(pauseObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        if (canTakePhoto)
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
