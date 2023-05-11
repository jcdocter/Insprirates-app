using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool canDisplayQuest;
    public bool questIsStarted;
    public bool isDone;

    public QuestData(Quest _data)
    {
        canDisplayQuest = _data.canDisplayQuest;
        questIsStarted = _data.startQuest;
        isDone = _data.isDone;
    }
}
