using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Scripts
{
    public bool hasFish;
    public bool hasRecruits;
    public bool checkOff = true;

    public int ID;

    [TextArea(3, 5)]
    public string[] scriptText;
}

public class Dialogue : MonoBehaviour
{
    public List<Scripts> scriptList;

    public TextMeshProUGUI dialogueBox;
    public GameObject previousButton;

    private Rules rules = new Rules();
    private int index = 0;
    private string[] text;
    private bool isQuestDone;

    private void Start()
    {
        dialogueBox = GetComponentInChildren<TextMeshProUGUI>();
        previousButton.SetActive(false);

        foreach (Scripts script in scriptList)
        {
            if (script.ID != PlayerPrefs.GetInt("questID"))
            {
                continue;
            }

            if(text == null)
            {
                SetTexts(script);
                isQuestDone = script.checkOff;
            }  
        }

        dialogueBox.text = text[index];
    }

    public void NextDialogue()
    {
        previousButton.SetActive(true);
        index++;

        if(index >= text.Length)
        {
            LastLine();
            return;
        }

        dialogueBox.text = text[index];
    }

    public void PreviousDialogue()
    {
        index--;

        dialogueBox.text = text[index];

        if(index == 0)
        {
            previousButton.SetActive(false);
        }
    }

    public void SetTexts(Scripts _script)
    {
        if(Inventory.GetInstance().amountOfFish > 0)
        {
            if(_script.hasFish)
            {
                text = _script.scriptText;
                return;
            }
        }
        else if(Inventory.GetInstance().amountOfRecruits > 0)
        {
            text = _script.scriptText;
            return;
        }
        else
        {
            text = _script.scriptText;
        }
    }

    private void LastLine()
    {
        rules.CheckOffQuest();
    }
}
