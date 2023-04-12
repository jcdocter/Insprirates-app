using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Confirm : MonoBehaviour
{
    public static int iconId;
    public static bool choseIcon = false;
    public TMP_InputField userName;

    public void nextScene()
    {
        if (userName.text != "" && choseIcon)
        {
            PlayerPrefs.SetString("username", userName.text);
            PlayerPrefs.SetInt("Icon", iconId);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
