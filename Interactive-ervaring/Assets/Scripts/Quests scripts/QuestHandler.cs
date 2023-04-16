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
    private float setTimer;

    private void Awake()
    {
        Tutorial.assignTutorial(tutorial);

        for (int i = 0; i < questList.Count; i++)
        {
            if(questList[i].canDisplayQuest)
            {
                DisplayQuest(questList[i]);
            }
        }
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddNewQuest("A1");
            AddNewQuest("A2");
            AddNewQuest("V1");
        }
    }

    public void AddNewQuest(string _questID)
    {
        Tutorial.scanTutorial.enabled = false;

        for (int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == _questID && !questList[i].canDisplayQuest)
            {
                DisplayQuest(questList[i]);
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

    private void DisplayQuest(Quest _quest)
    {
        GameObject questButton = Instantiate(button, buttonParent.transform);
        _quest.canDisplayQuest = true;
        questButton.GetComponent<QuestButton>().LoadData(_quest);
    }
}
