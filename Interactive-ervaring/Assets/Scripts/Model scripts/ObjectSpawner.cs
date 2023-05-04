using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Models
{
    public string[] ids;
    public GameObject model;

    public bool CheckID()
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (PlayerPrefs.GetString("modelID") == ids[i])
            {
                return true;
            }
        }

        return false;
    }
}

public class ObjectSpawner : MonoBehaviour
{
    public static string questID;
    public List<Models> modelList = new List<Models>();

    private void Start()
    {
        for (int i = 0; i < modelList.Count; i++)
        {
            if(modelList[i].CheckID())
            {
                GameObject model = Instantiate(modelList[i].model, transform.position, Quaternion.identity);
                model.transform.parent = transform;

                questID = PlayerPrefs.GetString("modelID");
            }
        }
    }
}
