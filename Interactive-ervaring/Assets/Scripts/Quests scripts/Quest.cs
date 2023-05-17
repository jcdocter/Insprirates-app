using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{

    [HideInInspector]
    public bool isDone;

    public string id;
    public List<string> ids;
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

        for(int i = 0; i < ids.Count; i++)
        {
            if (ids[i] == PlayerPrefs.GetString("questID"))
            {
                ids.RemoveAt(i);
            }
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