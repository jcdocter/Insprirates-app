using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI username;
    public Image playerIcon;
    public List<Sprite> iconList;

    public void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        username.text = data.name;
        playerIcon.sprite = iconList[data.iconId];
    }
}
