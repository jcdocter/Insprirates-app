using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QRID
{
    public string id;
    public bool activeQR;
}

[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{
    public bool isDone;

    public string description;

    public List<QRID> qrList = new List<QRID>();
    public Quest[] nextQuests;

    public Quest closeQuest;

    public void ActivateQuest()
    {
        foreach (QRID qr in qrList)
        {
            qr.activeQR = true;

            if (qr.id == PlayerPrefs.GetString("questID"))
            {
                this.isDone = true;
                qr.activeQR = false;
            }
        }

        PlayerPrefs.SetString("questID", " ");
    }
}