using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/new quest")]
public class Quest : ScriptableObject
{
    public string id;
    public bool isStory;
    public Quest nextQuest;
    public string description;
}

/*[CustomEditor(typeof(Quest))]
public class QuestScriptEditor : Editor
{
    private void OnInspectorGUI()
    {
        var script = target as Quest;

        script.isStory = GUILayout.Toggle(script.isStory, "Flag");

        if(script.isStory)
        {
            script.nextQuest = EditorGUILayout.ObjectField(script, target);
        }
    }
}*/
