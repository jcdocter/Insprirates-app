using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueBox;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject finalButton;

    protected string[] texts;
    protected int index = 0;

    private void Start()
    {
        SetScript();
    }

    public void NextDialogue()
    {
        previousButton.SetActive(true);
        index++;

        FinalText();

        if (index >= texts.Length)
        {
            EndCondition();
            return;
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

    protected void FinalText()
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

    protected abstract void EndCondition();

    protected virtual void SetScript()
    {
        previousButton.SetActive(false);
        finalButton.SetActive(false);

        dialogueBox.text = texts[index];
    }
}
