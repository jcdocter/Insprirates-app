using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Models> modelList = new List<Models>();

    private void Start()
    {
        for (int i = 0; i < modelList.Count; i++)
        {
            if(modelList[i].CheckID())
            {
                GameObject model = Instantiate(modelList[i].model, transform.position, Quaternion.identity);
                model.transform.parent = transform;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
}
