using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingPiece : Rules
{
    private ActionScanner actionScanner;

    private void Start()
    {
        SetRules();
        actionScanner = FindObjectOfType<ActionScanner>();
    }

    private void Update()
    {
        //input is for debugging. Can be removed later
        if(/*actionScanner.hasScanned || */Input.GetKeyDown(KeyCode.Space))
        {
            CheckOffQuest();
        }
    }
}
