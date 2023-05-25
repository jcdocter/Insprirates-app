using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{    
    public List<Quest> questList;
    public QuestTutorial questTutorial;

    private GameObject buttonParent;
    private FinishingQuest finishingQuest;

    private void Start()
    {
        buttonParent = FindObjectOfType<GridLayoutGroup>().gameObject;
        questTutorial.firstQuestTutorial.SetActive(false);
        
        finishingQuest = new FinishingQuest(buttonParent);
        LoadQuest();
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
            questList[i].isDone = finishingQuest.CheckProgress(questList[i]);    

            if (questList[i].isDone)
            {
                questTutorial.questTutorial.SetActive(false);
                questTutorial.telescopeTutorial.SetActive(false);
            }
        }

        PlayerPrefs.SetInt("confirmedID", 0);

        foreach (Transform child in buttonParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < questList.Count; i++)
        {
            if(questList[i].nextQuest == null)
            {
                continue;
            }

            if (!questList[i].nextQuest.isDone && questList[i].isDone)
            {
                finishingQuest.DisplayProgress(questList[i]);
            }
        }
    }
}
