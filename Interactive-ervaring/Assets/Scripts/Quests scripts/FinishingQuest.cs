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

        amount = InventoryValue(_quest);

        if ((_quest.amountToTrack <= amount && _quest.ID == PlayerPrefs.GetInt("confirmedID")) || _quest.isDone)
        {
            doneQuest = true;
        }

        return doneQuest;
    }

    public void DisplayProgress(Quest _quest)
    {
        _quest.descriptionObject.GetComponentInChildren<TextMeshProUGUI>().text = _quest.Description();

        GameObject button = GameObject.Instantiate(_quest.descriptionObject, displayParent.transform);

        if (_quest.isDone && _quest.nextQuest == null)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            button.transform.SetAsFirstSibling();
        }
    }

    public int InventoryValue(Quest _quest)
    {
        if(_quest.hasFish)
        {
            return Inventory.GetInstance().amountOfFish;
        }

        if (_quest.hasCrown)
        {
            return Inventory.GetInstance().amountOfCrownPieces;
        }

        if (_quest.hasRecruits)
        {
            return Inventory.GetInstance().amountOfRecruits;
        }

        return 0;
    }
}
