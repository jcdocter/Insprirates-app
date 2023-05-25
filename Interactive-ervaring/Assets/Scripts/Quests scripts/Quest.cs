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

    public string descriptionNextQuest;

    public List<QRID> qrList = new List<QRID>();
    public Quest[] nextQuests;

    public Quest closeQuest;

    public void ActivateQuest()
    {
        foreach (QRID qr in qrList)
        {
            if (qr.id == PlayerPrefs.GetString("questID"))
            {
                this.isDone = true;
                qr.activeQR = false;
            }
            else
            {
                qr.activeQR = true;
            }
        }

        PlayerPrefs.SetString("questID", " ");
    }
}