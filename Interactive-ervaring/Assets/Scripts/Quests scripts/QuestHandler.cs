using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    [HideInInspector]
    public bool isFirstQuest = true;
    
    public GameObject button;
    public GameObject tutorial;

    public List<Quest> questList;

    public QuestTutorial questTutorial;

    private GameObject buttonParent;
    private float setTimer;

    //For debugging. Can be deleted later
    private int debugIndex = 1;

    private void Awake()
    {
        buttonParent = FindObjectOfType<GridLayoutGroup>().gameObject;
    }

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

        if(!Debugger.OnDevice())
        {
            //Debug function
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddNewQuest("A" + debugIndex);
                debugIndex++;
            }
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
            if (questList[i].canDisplayQuest)
            {
                questTutorial.firstQuestTutorial.SetActive(false);
                isFirstQuest = false;

                DisplayQuest(questList[i]);
            }

            questList[i].ActivateQuest();
        }

        PlayerPrefs.SetString("buttonID", " ");
    }
}
