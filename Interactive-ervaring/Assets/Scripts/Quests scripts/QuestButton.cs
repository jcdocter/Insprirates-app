using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questDescription;
    public GameObject checkmark;
    public Image textBackground;

    private Quest quest;
    private QuestHandler questHandler;

    public void LoadData(Quest _quest)
    {
        questHandler = FindObjectOfType<QuestHandler>();
        quest = _quest;

        if (quest.isStory)
        {
            textBackground.color = new Color(255f/255f, 212f/255f, 180f/255f);
        }
        else
        {
            textBackground.color = new Color(181f/255f, 249f/255f, 249f/255f);
        }

        questDescription.text = quest.description;

        if (quest.isDone)
        {
            CheckOff();
        }
    }

    public void CheckOff()
    {
        if(!quest.isDone)
        {
            checkmark.SetActive(true);
            questDescription.fontStyle = FontStyles.Strikethrough;

            quest.isDone = true;
            transform.SetAsFirstSibling();

            if (quest.isStory)
            {
                questHandler.questList.Add(quest.nextQuest);
            }
        }
        else
        {
            checkmark.SetActive(false);
            questDescription.fontStyle = FontStyles.Normal;

            quest.isDone = false;
            transform.SetAsLastSibling();

            if (quest.isStory)
            {
                questHandler.questList.Remove(quest.nextQuest);
            }
        }

        questHandler.isFirstQuest = false;
        questHandler.questTutorial.firstQuestTutorial.SetActive(false);
        SaveSystem.SaveQuest();
    }
}
