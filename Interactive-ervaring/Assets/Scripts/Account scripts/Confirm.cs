using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;

public class Confirm : MonoBehaviour
{
    public bool choseIcon = false;
    public bool hasAccount;
    public int iconId;
    public TMP_InputField userName;

    public async void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        hasAccount = data.hasAccount;

        if (hasAccount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void nextScene()
    {
        if (userName.text != "" && choseIcon)
        {
            hasAccount = true;
            SaveSystem.SavePlayer(this);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
