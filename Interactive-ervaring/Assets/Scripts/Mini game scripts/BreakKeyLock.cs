using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakKeyLock : Rules
{
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
        if(Debugger.OnDevice())
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == this.gameObject.name)
                    {
                        playerTicks++;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerTicks++;
            }
        }

        if(playerTicks >= ticks)
        {
         //   animator.SetBool("canLoad", true);
        }
    }

    public void Unlocked()
    {
        CheckOffQuest();
    }
}
