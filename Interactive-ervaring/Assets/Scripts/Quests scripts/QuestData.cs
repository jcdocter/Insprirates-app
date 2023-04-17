using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool canDisplayQuest;
    public bool isDone;

    public QuestData(Quest _data)
    {
        canDisplayQuest = _data.canDisplayQuest;
        isDone = _data.isDone;
    }
}
