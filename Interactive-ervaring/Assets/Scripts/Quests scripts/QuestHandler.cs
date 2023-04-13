using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public GameObject button;
    public GameObject buttonParent;
    public GameObject tutorial;

    public List<Quest> questList;

    private List<Quest> displayList = new List<Quest>();
    private bool isFirstQuest = true;
    private float setTimer;

    private void Awake()
    {
        Tutorial.assignTutorial(tutorial);
    }

    private void Start()
    {
        Tutorial.questTutorial.enabled = true;
        Tutorial.telescopeTutorial.enabled = true;
        Tutorial.scanTutorial.enabled = false;
        Tutorial.firstQuestTutorial.enabled = false;
    }

    private void Update()
    {
        if(setTimer > 0 && isFirstQuest)
        {
            TutorialTimer();
        }
    }

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

        if(isFirstQuest)
        {
            setTimer = 10f;
        }

    }

    private void TutorialTimer()
    {
        setTimer -= Time.deltaTime;
        Tutorial.scanTutorial.enabled = false;
        Tutorial.firstQuestTutorial.enabled = true;

        if(setTimer <= 0f)
        {
            isFirstQuest = false;
            Destroy(tutorial);
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

        isFirstQuest = false;
        Destroy(tutorial);
    }
}
