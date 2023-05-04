using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ADone : MonoBehaviour
{
    public bool isScannable;
    public bool hasTimer;
    protected string questScene;

    protected GameObject timerObject;
    protected GameObject scanner;

    protected void Awake()
    {
        timerObject = FindObjectOfType<ActionScanner>().GetComponentInChildren<TextMeshProUGUI>().gameObject;
        scanner = FindObjectOfType<ActionScanner>().gameObject;
    }

    protected void SetRules()
    {
        scanner.SetActive(isScannable);
        timerObject.SetActive(hasTimer);

        questScene = "QuestPage";
    }

    protected void CheckOffQuest()
    {
        PlayerPrefs.SetString("buttonID", ObjectSpawner.questID);
        SceneManager.LoadScene(questScene);
    }
}
