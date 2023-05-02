using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Models
{
    public int id;
    public GameObject model;
}

public class ObjectSpawner : MonoBehaviour
{
    public List<Models> modelList = new List<Models>();

    public void Start()
    {
        for (int i = 0; i < modelList.Count; i++)
        {
            if(/*playerPrefs.GetInt("modelid")*/ 0 == modelList[i].id)
            {
                GameObject model = Instantiate(modelList[i].model, transform.position, Quaternion.identity);
                model.transform.parent = transform;
            }
        }
    }
}
