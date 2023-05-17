using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool questIsStarted;
    public bool isDone;

    public QuestData(Quest _data)
    {
        questIsStarted = _data.startQuest;
        isDone = _data.isDone;
    }
}
