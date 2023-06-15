using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialScripts
{
    public bool isEnd;

    [TextArea(3, 5)]
    public string[] tutorialTexts;
}

public class Tutorial : Dialogue
{
    public bool startOnly;
    public List<TutorialScripts> scriptList;

    private Rules rule = new Rules();
    private bool isEnd;

    protected override void EndCondition()
    {
        if(isEnd && !startOnly)
        {
            rule.CheckOffQuest();
        }
        else
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    protected override void SetScript()
    {
        if(startOnly)
        {
            RevertValue();
        }

        foreach (TutorialScripts script in scriptList)
        {
            if (!script.isEnd)
            {
                texts = script.tutorialTexts;
            }      
        }

        base.SetScript();
    }

    public void LastLine()
    {
        index = 0;

        RevertValue();
        SetScript();
    }
   
    private void RevertValue()
    {
        foreach (TutorialScripts script in scriptList)
        {
            script.isEnd = !script.isEnd;
        }

        isEnd = true;
    }
}
