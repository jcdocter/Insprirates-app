using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{    
    public GameObject treasureMap;
    public List<Quest> questList;
    public QuestTutorial questTutorial;

    private Animator animator;
    private GameObject buttonParent;
    private FinishingQuest finishingQuest;

    private void Start()
    {
        buttonParent = FindObjectOfType<GridLayoutGroup>().gameObject;
        animator = FindObjectOfType<Animator>();
        
        finishingQuest = new FinishingQuest(buttonParent);
        treasureMap.SetActive(DirectoryReader.DirectoryExist());
        LoadQuest();
    }

    public void ActivateCamera()
    {
        animator.SetBool("activateScope", true);
        questTutorial.telescopeTutorial.SetActive(false);
    }

    public void Recap()
    {
        SceneManager.LoadScene("RecapScreen");
    }

    private void LoadQuest()
    {
        if(SaveSystem.questList.Count == 0)
        {
            SaveSystem.questList = questList;
            SaveSystem.LoadQuest();
        }

        Progress();
    }

    private void Progress()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            questList[i].isDone = finishingQuest.CheckProgress(questList[i]);
        }

        foreach (Transform child in buttonParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].nextQuest == null)
            {
                if(questList[i].ID == PlayerPrefs.GetInt("confirmedID") || questList[i].isDone)
                {
                    finishingQuest.DisplayProgress(questList[i]);
                    questTutorial.questTutorial.SetActive(false);
                    questTutorial.telescopeTutorial.SetActive(false);
                }

                continue;
            }

            if (!questList[i].nextQuest.isDone && questList[i].isDone)
            {
                finishingQuest.DisplayProgress(questList[i]);

                questTutorial.questTutorial.SetActive(false);
                questTutorial.telescopeTutorial.SetActive(false);
            }
        }

        SaveSystem.SaveQuest();
        PlayerPrefs.SetInt("confirmedID", 0);
        PlayerPrefs.SetString("qrID", " ");
    }
}
