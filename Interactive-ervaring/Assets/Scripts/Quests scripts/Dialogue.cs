using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [TextArea(3,5)]
    public string[] text;

    private TextMeshProUGUI dialogueBox;

    private int index = 0;

    private void Start()
    {
        dialogueBox = GetComponentInChildren<TextMeshProUGUI>();

        dialogueBox.text = text[index];
    }

    public void NextDialogue()
    {
        index++;

        if(index >= text.Length)
        {
            Destroy(gameObject);
            Destroy(dialogueBox);

            return;
        }

        dialogueBox.text = text[index];
    }
}
