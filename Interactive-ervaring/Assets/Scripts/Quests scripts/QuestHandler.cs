using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public bool isFirstQuest = true;
    
    public GameObject button;
    public GameObject buttonParent;
    public GameObject tutorial;

    public List<Quest> questList;

    private List<Quest> displayList = new List<Quest>();
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
        if(setTimer > 0)
        {
            TutorialTimer();
        }
    }

    public void AddNewQuest(string _questID)
    {
        Tutorial.scanTutorial.enabled = false;

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
        if (isFirstQuest)
        {
            while (setTimer > 0f)
            {
                setTimer -= Time.deltaTime;
                Tutorial.firstQuestTutorial.enabled = true;
            }

            isFirstQuest = false;
        }
        else
        {

            Tutorial.firstQuestTutorial.enabled = false;
        }
    }

    private void DisplayQuest(int _id)
    {
        displayList.Add(questList[_id]);

        GameObject questButton = Instantiate(button, buttonParent.transform);
        questButton.GetComponent<QuestButton>().LoadData(_id, questList[_id].description, questList[_id].isStory);
    }

    public void ReplaceQuest(int _id)
    {
        if(questList[_id].isStory)
        {
            questList[_id] = questList[_id].nextQuest;
        }

        isFirstQuest = false;
        Tutorial.firstQuestTutorial.enabled = false;
    }
}
