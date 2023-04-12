using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public GameObject button;
    public GameObject buttonParent;

    public List<Quest> questList;

    private List<Quest> displayList = new List<Quest>();

    public void AddNewQuest(string _questID)
    {
        for (int i = 0; i < displayList.Count; i++)
        {
            if(displayList[i].id == _questID)
            {
                return;
            }
        }

        for (int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == _questID)
            {
                DisplayQuest(i);
            }
        }
    }

    private void DisplayQuest(int _id)
    {
        displayList.Add(questList[_id]);

        GameObject questButton = Instantiate(button, buttonParent.transform);
        questButton.GetComponent<QuestButton>().LoadData(_id, questList[_id].description);
    }

    public void ReplaceQuest(int _id)
    {
        if(questList[_id].isStory)
        {
            questList[_id] = questList[_id].nextQuest;
        }
    }
}
