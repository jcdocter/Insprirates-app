using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestHandler : MonoBehaviour
{
    public bool isFirstQuest = true;
    
    public GameObject button;
    public GameObject buttonParent;
    public GameObject tutorial;

    public List<Quest> questList;

    public QuestTutorial questTutorial;

    private float setTimer;
    private int debugIndex = 1;

    private void Start()
    {
        questTutorial.questTutorial.SetActive(true);
        questTutorial.telescopeTutorial.SetActive(true);
        questTutorial.firstQuestTutorial.SetActive(false);
        
        LoadQuest();

        if (PlayerPrefs.GetString("questID") != "")
        {
            AddNewQuest(PlayerPrefs.GetString("questID"));
        }
    }

    private void Update()
    {
        if(setTimer > 0)
        {
            TutorialTimer();
        }

        //Debug function
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddNewQuest("A" + debugIndex);
            debugIndex++;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AddNewQuest("V1");
            debugIndex++;
        }
    }

    public void AddNewQuest(string _questID)
    {
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
        questTutorial.questTutorial.SetActive(false);
        questTutorial.telescopeTutorial.SetActive(false);

        GameObject questButton = Instantiate(button, buttonParent.transform);
        _quest.canDisplayQuest = true;

        questButton.GetComponent<QuestButton>().LoadData(_quest);

        SaveSystem.SaveQuest();
    }

    public void ActivateCamera()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadQuest()
    {
        SaveSystem.questList = questList;
        SaveSystem.LoadQuest();

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].canDisplayQuest)
            {
                questTutorial.firstQuestTutorial.SetActive(false);
                isFirstQuest = false;

                DisplayQuest(questList[i]);
            }
        }

        PlayerPrefs.SetString("buttonID", " ");
    }
}
