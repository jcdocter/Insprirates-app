using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questDescription;

    public void CheckOff()
    {
        questDescription.fontStyle = FontStyles.Strikethrough;
        this.enabled = false;
    }
}
