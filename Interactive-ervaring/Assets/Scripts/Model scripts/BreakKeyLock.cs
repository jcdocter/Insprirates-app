using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakKeyLock : MonoBehaviour, IDone
{
    public string questSceneProperty { get; set; }

    public GameObject creatureObject;
    private Animator animator;
    private ObjectSpawner spawner;
    private bool canSpin;

    private int ticks;
    private int playerTicks;

    private void Start()
    {
        questSceneProperty = "QuestPage";

        ticks = Random.Range(2, 10);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerTicks++;
            //canSpin = !canSpin;
        }

        if(playerTicks >= ticks)
        {
            animator.SetBool("canLoad", true);
        }
    }

    public void Unlocked()
    {
        CheckOffQuest();
    }

    public void CheckOffQuest()
    {
        PlayerPrefs.SetString("buttonID", ObjectSpawner.questID);
        SceneManager.LoadScene(questSceneProperty);
    }
}
