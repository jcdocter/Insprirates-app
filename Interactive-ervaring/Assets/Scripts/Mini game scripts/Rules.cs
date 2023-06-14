using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    [HideInInspector]
    public PhotoCapture photoCapture;

    [HideInInspector]
    public Canvas pauseScreen;
    
    public GameObject rewardObject;
    public Canvas pauseObject;

    private RecCamera recCam;
    private bool canStartGame = false;

    public void SetRules()
    {
        recCam = GameObject.FindObjectOfType<RecCamera>();
        recCam.canSwitchCam = false;
        pauseScreen = GameObject.Instantiate(pauseObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        pauseScreen.GetComponent<Canvas>().worldCamera = Camera.main;

        SetPicture(false);
    }

    public void SetPicture(bool _activatePictureMode)
    {
        if(photoCapture == null)
        {
            photoCapture = GameObject.FindObjectOfType<PhotoCapture>();
        }

        photoCapture.gameObject.SetActive(_activatePictureMode);
    }

    public bool StartGame()
    {
        if (Input.GetMouseButtonDown(0))
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
        PlayerPrefs.SetInt("confirmedID", PlayerPrefs.GetInt("questID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
