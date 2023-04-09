using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Confirm : MonoBehaviour
{
    public TMP_InputField userName;

    public void nextScene()
    {
        if (userName.text != "")
        {
            PlayerPrefs.SetString("username", userName.text);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
