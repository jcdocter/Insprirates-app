using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{
    public string id;
    public bool isMainStory;
    public bool isDone;
    public string description;
}
