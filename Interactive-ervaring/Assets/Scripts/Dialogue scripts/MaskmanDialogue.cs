using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskmanDialogue : Dialogue
{
    public bool endDialogue;
    public string[] maskDialogue;

    protected override void EndCondition()
    {
        endDialogue = true;
        this.gameObject.SetActive(false);
    }

    protected override void SetScript()
    {
        texts = maskDialogue;
        base.SetScript();
    }
}
