using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    [HideInInspector]
    public PhotoCapture photoCapture;

    public GameObject rewardObject;

    [HideInInspector]
    public Canvas pauseScreen;
    
    public Canvas pauseObject;
    public Canvas instructionCanavas;
    private RecCamera recCam;
    private bool canStartGame = false;

    public void SetRules()
    {
        recCam = GameObject.FindObjectOfType<RecCamera>();
        recCam.canSwitchCam = false;
        instructionCanavas = GameObject.Instantiate(instructionCanavas, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        pauseScreen = GameObject.Instantiate(pauseObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        pauseScreen.GetComponent<Canvas>().worldCamera = Camera.main;
        photoCapture = GameObject.FindObjectOfType<PhotoCapture>();

        SetPicture(false);
    }

    public void SetPicture(bool _activatePictureMode)
    {
        photoCapture.gameObject.SetActive(_activatePictureMode);
    }

    public bool StartGame()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pauseScreen.enabled = false;
            canStartGame = true;
        }

        return canStartGame;
    }

    public void ShowReward(Transform _display)
    {
        GameObject.Instantiate(rewardObject, _display.position, rewardObject.transform.localRotation);
    }

    public void CheckOffQuest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("confirmedID", PlayerPrefs.GetInt("questID"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
}
