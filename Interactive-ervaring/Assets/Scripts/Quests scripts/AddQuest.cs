using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddQuest : MonoBehaviour
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
                displayList.Add(questList[i]);

                DisplayQuest(questList[i].description);
            }
        }
    }

    private void DisplayQuest(string _description)
    {
        GameObject questButton = Instantiate(button, buttonParent.transform);
        questButton.GetComponent<QuestButton>().questDescription.text = _description;
    }
}
