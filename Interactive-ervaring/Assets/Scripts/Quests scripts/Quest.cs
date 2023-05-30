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
    public bool showDescription;

    public bool hasFish;
    public bool hasRecruits;
    public int amountToTrack;
    public string descriptionNextQuest;

    public GameObject descriptionObject;

    public List<QRID> qrList = new List<QRID>();
    public Quest nextQuest;

    public string Description()
    {
        // ID == needs to change
        if(hasFish)
        {
            return $"{descriptionNextQuest} {Inventory.GetInstance().amountOfFish} / {amountToTrack}";
        }
        else if(hasRecruits)
        {
            return $"{descriptionNextQuest} {Inventory.GetInstance().amountOfRecruits} / {amountToTrack}";
        }

        return descriptionNextQuest;
    }
}