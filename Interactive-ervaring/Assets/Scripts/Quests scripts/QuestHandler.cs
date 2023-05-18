using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    [HideInInspector]
    public static bool isFirstQuest = true;
    
    public GameObject button;
    public GameObject tutorial;

    public List<Quest> questList;

    public QuestTutorial questTutorial;

    private GameObject buttonParent;
    private float setTimer;

    private static bool questTutorialActive = true;
    private static bool telescopeTutorialActive = true;

    private void Start()
    {
        buttonParent = FindObjectOfType<GridLayoutGroup>().gameObject;
        questTutorial.questTutorial.SetActive(questTutorialActive);
        questTutorial.telescopeTutorial.SetActive(telescopeTutorialActive);
        questTutorial.firstQuestTutorial.SetActive(false);
        
        LoadQuest();
    }

    private void Update()
    {
        if(setTimer > 0)
        {
            TutorialTimer();
        }
    }

    private void TutorialTimer()
    {
        if (isFirstQuest)
        {
            while (setTimer > 0f)
            {
                setTimer -= Time.deltaTime;
                questTutorial.firstQuestTutorial.SetActive(true);
            }

            isFirstQuest = false;
        }
        else
        {
            questTutorial.firstQuestTutorial.SetActive(false);
        }
    }

    private void DisplayQuest(Quest _quest)
    {
        questTutorialActive = false;

        GameObject questButton = Instantiate(button, buttonParent.transform);
        questButton.GetComponentInChildren<TextMeshProUGUI>().text = _quest.description;

        SaveSystem.SaveQuest();
    }

    public void ActivateCamera()
    {
        telescopeTutorialActive = false;
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

            if (questList[i].isDone)
            {
                DisplayQuest(questList[i]);

                foreach(Quest quest in questList[i].nextQuests)
                {
                    quest.qrList = questList[i].qrList;
                    questList.Add(quest);
                }

                isFirstQuest = false;
                questList.RemoveAt(i);
            }
        }
    }
}
