using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public static bool firstTime = false;
    public bool isTutorial;
    public List<Scripts> scriptList;

    public TextMeshProUGUI dialogueBox;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject finalButton;

    private Rules rules = new Rules();
    private int index = 0;
    private string[] texts;

    private void Start()
    {
        previousButton.SetActive(false);
        finalButton.SetActive(false);

        if (firstTime)
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        if(isTutorial)
        {
            texts = scriptList[0].scriptText;
            dialogueBox.text = texts[index];

            return;
        }

        rules.SetPicture(false);    

        foreach (Scripts script in scriptList)
        {
            if (script.ID != PlayerPrefs.GetInt("questID"))
            {
                continue;
            }

           SetTexts(script);
        }

        dialogueBox.text = texts[index];
    }

    public void NextDialogue()
    {
        previousButton.SetActive(true);
        index++;

        FinalText();

        if (index >= texts.Length)
        {
            if(isTutorial)
            {
                firstTime = true;
                this.transform.parent.gameObject.SetActive(false);
                return;
            }
            else
            {
                rules.CheckOffQuest();
                return;
            }
        }

        dialogueBox.text = texts[index];
    }

    public void PreviousDialogue()
    {
        index--;
        FinalText();

        dialogueBox.text = texts[index];

        if(index == 0)
        {
            previousButton.SetActive(false);
        }
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

    private void FinalText()
    {
        if (index == texts.Length - 1)
        {
            finalButton.SetActive(true);
            nextButton.SetActive(false);
        }
        else
        {
            finalButton.SetActive(false);
            nextButton.SetActive(true);
        }
    }
}
