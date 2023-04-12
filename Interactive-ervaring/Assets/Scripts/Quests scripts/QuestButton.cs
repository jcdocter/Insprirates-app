using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI questDescription;
    private QuestHandler questHandler;

    public void LoadData(int _id, string _description)
    {
        questHandler = FindObjectOfType<QuestHandler>();
        id = _id;
        questDescription.text = _description;
    }

    public void CheckOff()
    {
        questDescription.fontStyle = FontStyles.Strikethrough;
        this.enabled = false;

        questHandler.ReplaceQuest(id);
    }
}
