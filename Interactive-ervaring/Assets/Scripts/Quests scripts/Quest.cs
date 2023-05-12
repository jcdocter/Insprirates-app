using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{
    [HideInInspector]
    public bool canDisplayQuest;

    [HideInInspector]
    public bool isDone;

    public string id;
    public bool startQuest;

    public string description;

    public Quest[] neededQuests;

    public void ActivateQuest()
    {
        if(neededQuests == null)
        {
            return;
        }

        foreach(Quest quest in neededQuests)
        {
            if (!quest.isDone)
            {
                this.startQuest = false;
                return;
            }
        }

        this.startQuest = true;
    }
}