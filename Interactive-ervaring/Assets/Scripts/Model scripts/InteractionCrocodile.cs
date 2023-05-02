using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionCrocodile : MonoBehaviour
{
    public GameObject creatureObject;
    private Animator animator;
    private bool canSpin;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            canSpin = !canSpin;
        }

        animator.SetBool("canLoad", canSpin);
    }
}
