using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [TextArea(3,5)]
    public string[] text;

    public TextMeshProUGUI dialogueBox;
    public GameObject previousButton;

    private int index = 0;

    private void Start()
    {
        dialogueBox = GetComponentInChildren<TextMeshProUGUI>();
        previousButton.SetActive(false);

        dialogueBox.text = text[index];
    }

    public void NextDialogue()
    {
        previousButton.SetActive(true);
        index++;

        Debug.Log(index);

        if(index >= text.Length)
        {
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
}
