using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{    
    public GameObject button;

    public List<Quest> questList;

    public QuestTutorial questTutorial;

    private GameObject buttonParent;

    private void Start()
    {
        buttonParent = FindObjectOfType<GridLayoutGroup>().gameObject;
        questTutorial.firstQuestTutorial.SetActive(false);
        
        LoadQuest();
    }

    private void DisplayQuest(Quest _quest)
    {
        GameObject questButton = Instantiate(button, buttonParent.transform);
        questButton.GetComponentInChildren<TextMeshProUGUI>().text = _quest.descriptionNextQuest;

        SaveSystem.SaveQuest();
    }

    public void ActivateCamera()
    {
        questTutorial.telescopeTutorial.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Recap()
    {
        SceneManager.LoadScene("RecapScreen");
    }

    private void LoadQuest()
    {
        SaveSystem.questList = questList;
        SaveSystem.LoadQuest();

        for (int i = 0; i < questList.Count; i++)
        {
            questList[i].ActivateQuest();

            if (!questList[i].isDone)
            {
                continue;
            }

            questTutorial.questTutorial.SetActive(false);
            questTutorial.telescopeTutorial.SetActive(false);

            DisplayQuest(questList[i]);

            foreach(Quest quest in questList[i].nextQuests)
            {
                quest.qrList = questList[i].qrList;
                questList.Add(quest);
            }

            questList.RemoveAt(i);
        }
    }
}
