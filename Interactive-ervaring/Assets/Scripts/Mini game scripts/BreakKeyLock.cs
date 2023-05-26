using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakKeyLock : MonoBehaviour
{
    public Rules rules;
    private Animator animator;

    private int ticks;
    private int playerTicks;

    private void Start()
    {
        rules.SetRules();

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
            animator.SetBool("canLoad", true);
        }
    }

    public void Unlocked()
    {
        rules.CheckOffQuest();
    }
}
