using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool isDone;
//    public bool showDescription;

    public QuestData(Quest _data)
    {
        this.isDone = _data.isDone;
 //       this.showDescription = _data.showDescription;
    }
}

