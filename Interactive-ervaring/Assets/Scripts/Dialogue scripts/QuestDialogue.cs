using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scripts
{
    public bool needsFish;
    public int ID;

    [TextArea(3, 5)]
    public string[] scriptText;
}

public class QuestDialogue : Dialogue
{
    public List<Scripts> scriptList;

    private Rules rules = new Rules();

    protected override void SetScript()
    {
        rules.SetPicture(false);

        foreach (Scripts script in scriptList)
        {
            if (script.ID != PlayerPrefs.GetInt("questID"))
            {
                continue;
            }

            SetTexts(script);
        }

        base.SetScript();
    }

    private void SetTexts(Scripts _script)
    {
        if (_script.needsFish)
        {
            if (Inventory.GetInstance().amountOfFish > 0)
            {
                texts = _script.scriptText;
                return;
            }
        }
        else
        {
            texts = _script.scriptText;
        }
    }

    protected override void EndCondition()
    {
        rules.CheckOffQuest();
    }
}
