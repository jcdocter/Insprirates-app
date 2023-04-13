using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI questDescription;
    public Sprite[] buttonImage;
    private QuestHandler questHandler;

    public void LoadData(int _id, string _description, bool _isStory)
    {
        questHandler = FindObjectOfType<QuestHandler>();

        if(_isStory)
        {
            this.gameObject.transform.GetComponent<Image>().sprite = buttonImage[1];
        }
        else
        {
            this.gameObject.transform.GetComponent<Image>().sprite = buttonImage[0];
        }

        id = _id;
        questDescription.text = _description;
    }

    public void CheckOff()
    {
        questDescription.fontStyle = FontStyles.Strikethrough;
        this.enabled = false;

        questHandler.ReplaceQuest(id);
    }
}
