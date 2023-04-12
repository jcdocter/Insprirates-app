using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI username;
    public Image playerIcon;
    public List<Sprite> iconList;

    public void Start()
    {
        username.text = PlayerPrefs.GetString("username");
        playerIcon.sprite = iconList[PlayerPrefs.GetInt("Icon")];
    }
}
