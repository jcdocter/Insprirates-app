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
    public int ID;
    public bool isDone;
//    public bool showDescription;

    public bool hasFish;
    public bool hasCrown;
    public bool hasRecruits;
    public int amountToTrack;
    public string descriptionNextQuest;

    public string QRID;
    public GameObject descriptionObject;
    public Quest nextQuest;

    public string Description()
    {
        if(hasFish)
        {
            return $"{descriptionNextQuest} {Inventory.GetInstance().amountOfFish} / {amountToTrack}";
        }
        else if(hasCrown)
        {
            return $"{descriptionNextQuest} {Inventory.GetInstance().amountOfCrownPieces} / {amountToTrack}";
        }
        else if (hasRecruits)
        {
            return $"{descriptionNextQuest} {Inventory.GetInstance().amountOfRecruits} / {amountToTrack}";
        }

        return descriptionNextQuest;
    }

/*    public void DeactivatedQR()
    {
        if (ID == PlayerPrefs.GetInt("confirmedID"))
        {
            CheckQRList(qrList);

            if(nextQuest != null) 
            {
                CheckQRList(nextQuest.qrList);
            }
        }
    }

    private void CheckQRList(List<QRID> _qrList)
    {
        foreach (QRID qr in _qrList)
        {
            if (qr.id == PlayerPrefs.GetString("qrID"))
            {
                qr.activeQR = false;
            }
            else
            {
                qr.activeQR = true;
            }
        }
    }*/
}