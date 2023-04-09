using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI username;

    public void Start()
    {
        username.text = PlayerPrefs.GetString("username");
    }
}
