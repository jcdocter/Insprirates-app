using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishingQuest
{
    private GameObject displayParent;

    public FinishingQuest(GameObject _buttonParent)
    {
       this.displayParent = _buttonParent;
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
        _quest.descriptionObject.GetComponentInChildren<TextMeshProUGUI>().text = _quest.Description();

        GameObject button = GameObject.Instantiate(_quest.descriptionObject, displayParent.transform);

/*        if (_quest.isDone && _quest.nextQuest == null)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            button.transform.SetAsFirstSibling();
        }*/
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
