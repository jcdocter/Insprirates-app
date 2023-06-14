using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct QuestField
{
    public Quest[] quests;
    public GameObject field;
}

public class QuestHandler : MonoBehaviour
{    
    public GameObject treasureMap;
    public List<Quest> questList = new List<Quest>();
    public List<QuestField> questFieldList = new List<QuestField>();

    private Animator animator;
    private FinishingQuest finishingQuest;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        animator = FindObjectOfType<Animator>();
        finishingQuest = new FinishingQuest(questFieldList);
        finishingQuest.DeactivateProgress();
        LoadQuest();

        treasureMap.SetActive(DirectoryReader.DirectoryExist());
    }

    public void ActivateCamera()
    {        
        animator.SetBool("activateScope", true);
    }

    public void Recap()
    {
        if(!FindObjectOfType<Dialogue>().gameObject.activeSelf)
        {
            SceneManager.LoadScene("RecapScreen");
        }
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
            finishingQuest.DisplayProgress(questList[i]);
        }

        SaveSystem.SaveQuest();
        PlayerPrefs.SetInt("confirmedID", 0);
        PlayerPrefs.SetString("qrID", " ");
    }
}
