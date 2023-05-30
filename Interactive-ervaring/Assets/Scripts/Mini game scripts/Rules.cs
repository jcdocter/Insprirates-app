using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Rules
{
    [HideInInspector]
    public Canvas pauseScreen;
    
    public Canvas pauseObject;
    private RecCamera recCam;
    private bool canStartGame = false;

    public void SetRules()
    {
        recCam = GameObject.FindObjectOfType<RecCamera>();
        recCam.canSwitchCam = false;
        pauseScreen = GameObject.Instantiate(pauseObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
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

    public void CheckOffQuest()
    {
        PlayerPrefs.SetInt("confirmedID", PlayerPrefs.GetInt("questID"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
