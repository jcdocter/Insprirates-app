using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{
    public string id;
    public bool canDisplayQuest;
    public bool isDone;
    public string description;
    public int prefabID;

    public bool isStory;

    [HideInInspector]
    public Quest nextQuest;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Quest script = (Quest)target;

        if (script.isStory)
        {
            script.nextQuest = EditorGUILayout.ObjectField("Next Quest", script.nextQuest, typeof(Quest), true) as Quest;
        }
    }
}
#endif