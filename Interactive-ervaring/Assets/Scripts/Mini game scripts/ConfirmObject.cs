using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmObject : MonoBehaviour
{
    private MaskmanDialogue maskmanDialogue;
    private Rules rules = new Rules();
    private Swipe swipe = new Swipe();

    private void Start()
    {
        maskmanDialogue = GetComponentInChildren<MaskmanDialogue>();
        rules.SetPicture(false);
    }

    private void Update()
    {
        if(!maskmanDialogue.endDialogue)
        {
            return;
        }

        if(!Debugger.OnDevice())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Inventory.GetInstance().amountOfRecruits++;

                rules.CheckOffQuest();
            }
        }

        Swipe();
    }

    private void Swipe()
    {
        if(swipe.CheckSwipe())
        {
            Inventory.GetInstance().amountOfRecruits++;
            rules.CheckOffQuest();
        }
    }
}
