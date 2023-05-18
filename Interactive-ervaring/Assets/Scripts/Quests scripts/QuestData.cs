using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool isDone;

    public QuestData(Quest _data)
    {
        isDone = _data.isDone;
    }
}
