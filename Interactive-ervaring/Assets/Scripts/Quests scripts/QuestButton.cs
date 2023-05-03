using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct ButtonLayout
{
    public Sprite checkmarkBox;
    public Sprite textBackground;
}

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questDescription;
    public GameObject checkmark;

    public Image checkmarkBox;
    public Image textBackground;

    public List<ButtonLayout> buttonLayoutList = new List<ButtonLayout>();

    private Quest quest;
    private QuestHandler questHandler;
    private string questScene = "QuestScene";

    public void LoadData(Quest _quest)
    {
        questHandler = FindObjectOfType<QuestHandler>();
        quest = _quest;

        if (quest.isStory)
        {
            checkmarkBox.sprite = buttonLayoutList[1].checkmarkBox;
            textBackground.sprite = buttonLayoutList[1].textBackground;
        }
        else
        {
            checkmarkBox.sprite = buttonLayoutList[0].checkmarkBox;
            textBackground.sprite = buttonLayoutList[0].textBackground;
        }

        questDescription.text = quest.description;

        Debug.Log(quest.id + " " + quest.isDone);

        if (quest.isDone || _quest.id == PlayerPrefs.GetString("buttonID"))
        {
            CheckOff();
        }

        PlayerPrefs.SetString("buttonID", " ");
    }

    public void DoQuest()
    {
        PlayerPrefs.SetString("modelID", quest.id);
        SceneManager.LoadScene(questScene);
    }

    public void CheckOff()
    {
        checkmark.SetActive(true);
        questDescription.fontStyle = FontStyles.Strikethrough;

        quest.isDone = true;
        transform.SetAsFirstSibling();

        if (quest.isStory)
        {
            questHandler.questList.Add(quest.nextQuest);
        }

/*        else
        {
            checkmark.SetActive(false);
            questDescription.fontStyle = FontStyles.Normal;

            quest.isDone = false;
            transform.SetAsLastSibling();

            if (quest.isStory)
            {
                questHandler.questList.Remove(quest.nextQuest);
            }
        }*/

        questHandler.isFirstQuest = false;
        questHandler.questTutorial.firstQuestTutorial.SetActive(false);
        SaveSystem.SaveQuest();
    }
}
