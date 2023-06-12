using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Scripts
{
    public bool needsFish;
    public int ID;

    [TextArea(3, 5)]
    public string[] scriptText;
}

public class Dialogue : MonoBehaviour
{
    public List<Scripts> scriptList;

    public TextMeshProUGUI dialogueBox;

    public GameObject previousButton;
    public GameObject nextButton;

    private Rules rules = new Rules();
    private int index = 0;
    private string[] text;

    private void Start()
    {
        rules.SetPicture(false);
        previousButton.SetActive(false);

        foreach (Scripts script in scriptList)
        {
            if (script.ID != PlayerPrefs.GetInt("questID"))
            {
                continue;
            }

           SetTexts(script);
        }

        dialogueBox.text = text[index];
    }

    public void NextDialogue()
    {
        previousButton.SetActive(true);
        index++;

        if(index >= text.Length)
        {
            rules.CheckOffQuest();
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
        if (_script.needsFish)
        {
            if (Inventory.GetInstance().amountOfFish > 0)
            {
                text = _script.scriptText;
                return;
            }
        }
        else
        {
            text = _script.scriptText;
        }
    }
}
