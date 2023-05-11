using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int iconId;
    public bool hasAccount;

    public PlayerData(Confirm _data)
    {
        name = _data.userName.text;
        iconId = _data.iconId;
        hasAccount = _data.hasAccount;
    }
}
