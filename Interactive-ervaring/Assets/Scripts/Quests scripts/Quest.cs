using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{

    [HideInInspector]
    public bool isDone;

    public string id;
    public string[] ids;
    public bool startQuest;

    public string description;

    public Quest[] neededQuests;

    public Quest closeQuest;

    public void ActivateQuest()
    {
        if(neededQuests == null)
        {
            return;
        }

        foreach(Quest quest in neededQuests)
        {
            if (!quest.isDone || closeQuest.startQuest)
            {
                this.startQuest = false;
                return;
            }
        }

        this.startQuest = true;
    }

    public string GetID()
    {
        foreach(string id in ids)
        {
            return id;
        }

        return null;
    }
}