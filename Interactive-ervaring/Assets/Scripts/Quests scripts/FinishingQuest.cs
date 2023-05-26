using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishingQuest
{
    private GameObject displayParent;
    private int amount = 0;

    public FinishingQuest(GameObject _buttonParent)
    {
       this.displayParent = _buttonParent;
    }

    public bool CheckProgress(Quest _quest)
    {
        bool doneQuest = false;
        if (_quest.hasFish || _quest.hasRecruits)
        {
            amount = _quest.hasFish ? Inventory.GetInstance().amountOfFish : Inventory.GetInstance().amountOfRecruits;
        }

        if ((_quest.amountToTrack <= amount && _quest.ID == PlayerPrefs.GetInt("confirmedID")) || _quest.isDone)
        {
            doneQuest = true;
        }

        SaveSystem.SaveQuest();
        return doneQuest;
    }

    public void DisplayProgress(Quest _quest)
    {
        _quest.descriptionObject.GetComponentInChildren<TextMeshProUGUI>().text = _quest.Description();

        GameObject.Instantiate(_quest.descriptionObject, displayParent.transform);
    }
}
