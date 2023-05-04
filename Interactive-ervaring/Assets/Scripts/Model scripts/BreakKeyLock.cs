using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakKeyLock : ADone
{
    public GameObject creatureObject;
    private Animator animator;

    private int ticks;
    private int playerTicks;

    private void Start()
    {
        SetRules();

        ticks = Random.Range(2, 10);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerTicks++;
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
}
