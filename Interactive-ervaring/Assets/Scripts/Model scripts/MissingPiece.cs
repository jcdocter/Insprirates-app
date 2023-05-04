using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissingPiece : ADone
{
    private ActionScanner actionScanner;

    private void Start()
    {
        SetRules();
        actionScanner = FindObjectOfType<ActionScanner>();
    }

    private void Update()
    {
        if(actionScanner.hasScanned || Input.GetKeyDown(KeyCode.Space))
        {
            CheckOffQuest();
        }
    }
}
