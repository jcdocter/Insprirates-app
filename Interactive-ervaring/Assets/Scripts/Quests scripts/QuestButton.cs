using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questDescription;

    public void LoadData(Quest _quest)
    {
        questDescription.text = _quest.description;
    }
}
