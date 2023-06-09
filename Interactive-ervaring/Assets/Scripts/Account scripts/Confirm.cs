using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;

public class Confirm : MonoBehaviour
{
    [HideInInspector]
    public bool choseIcon = false;

    [HideInInspector]
    public bool hasAccount;

    [HideInInspector]
    public int iconId;

    public TMP_InputField userName;

    private void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        hasAccount = data.hasAccount;

        if (hasAccount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void NextScene()
    {
        if (userName.text != "" && choseIcon)
        {
            hasAccount = true;
            SaveSystem.SavePlayer(this);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
