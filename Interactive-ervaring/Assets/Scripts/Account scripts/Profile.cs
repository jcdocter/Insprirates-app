using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public List<Sprite> iconList;
    
    private TextMeshProUGUI username;
    private Image playerIcon;

    private void Awake()
    {
        playerIcon = GetComponentInChildren<Image>();
        username = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        username.text = data.name;
        playerIcon.sprite = iconList[data.iconId];
    }
}
