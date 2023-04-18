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
    private int debugIndex = 1;

    private void Start()
    {
        Tutorial.assignTutorial(tutorial);

        Tutorial.questTutorial.enabled = true;
        Tutorial.telescopeTutorial.enabled = true;
        Tutorial.scanTutorial.enabled = false;
        Tutorial.firstQuestTutorial.enabled = false;

        SaveSystem.questList = questList;
        SaveSystem.LoadQuest();

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].canDisplayQuest)
            {
                Tutorial.questTutorial.enabled = false;
                Tutorial.telescopeTutorial.enabled = false;
                Tutorial.firstQuestTutorial.enabled = false;
                isFirstQuest = false;

                DisplayQuest(questList[i]);
            }
        }
    }

    private void Update()
    {
        if(setTimer > 0)
        {
            TutorialTimer();
        }

        //Debug function
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddNewQuest("A" + debugIndex);
            debugIndex++;
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

        if(_quest.isDone)
        {
            questButton.GetComponent<QuestButton>().CheckOff();
        }

        SaveSystem.SaveQuest();
    }
}
