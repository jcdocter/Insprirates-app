using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questDescription;
    public Sprite[] buttonImage;

    private Quest quest;
    private QuestHandler questHandler;

    public void LoadData(Quest _quest)
    {
        questHandler = FindObjectOfType<QuestHandler>();
        quest = _quest;

        if(quest.isStory)
        {
            this.gameObject.transform.GetComponent<Image>().sprite = buttonImage[1];
        }
        else
        {
            this.gameObject.transform.GetComponent<Image>().sprite = buttonImage[0];
        }

        questDescription.text = quest.description;

        if (quest.isDone)
        {
            CheckOff();
            return;
        }
    }

    public void CheckOff()
    {
        if(!quest.isDone)
        {
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
            questDescription.fontStyle = FontStyles.Normal;

            quest.isDone = false;
            transform.SetAsLastSibling();

            if (quest.isStory)
            {
                questHandler.questList.Remove(quest.nextQuest);
            }
        }

        questHandler.isFirstQuest = false;
        Tutorial.instance.firstQuestTutorial.SetActive(false);
        SaveSystem.SaveQuest();
    }
}
