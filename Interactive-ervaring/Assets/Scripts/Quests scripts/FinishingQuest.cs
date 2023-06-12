using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishingQuest
{
    private List<QuestField> questFieldList;

    public FinishingQuest(List<QuestField> _questFieldList) 
    { 
        this.questFieldList = _questFieldList;
    }

    public bool CheckProgress(Quest _quest)
    {
        bool doneQuest = false;

        if ((InventoryValue(_quest) && _quest.ID == PlayerPrefs.GetInt("confirmedID")) || _quest.isDone)
        {
            doneQuest = true;
        }

        return doneQuest;
    }

    public void DisplayProgress(Quest _quest)
    {
        for (int i = 0; i < questFieldList.Count; i++)
        {
            for (int j = 0; j < questFieldList[i].quests.Length; j++)
            {
                if (_quest.isDone && _quest.ID == questFieldList[i].quests[j].ID)
                {
                    questFieldList[i].field.SetActive(true);
                    questFieldList[i].field.GetComponentInChildren<TextMeshProUGUI>().text = _quest.Description();
                }
            }
        }
    }

    public void DeactivateProgress()
    {
        for (int i = 0; i < questFieldList.Count; i++)
        {
            questFieldList[i].field.SetActive(false);
        }
    }

    private bool InventoryValue(Quest _quest)
    {
        if(_quest.hasFish)
        {
           if(Inventory.GetInstance().amountOfFish >= _quest.amountToTrack)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
