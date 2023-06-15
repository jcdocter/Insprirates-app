using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Models
{
    public Quest[] quests;
    public GameObject model;

    public bool CheckID()
    {
        foreach (Quest quest in quests)
        {
            if (PlayerPrefs.GetInt("questID") == quest.ID)
            {
                return true;
            }
        }

        return false;
    }
}

public class ObjectSpawner : MonoBehaviour, IGoBack
{
    public List<Models> modelList = new List<Models>();

    private void Start()
    {
        for (int i = 0; i < modelList.Count; i++)
        {
            if(modelList[i].CheckID())
            {
                //GameObject model = Instantiate(modelList[i].model, transform.position, transform.rotation);
                GameObject model = Instantiate(modelList[i].model, modelList[i].model.transform.position, modelList[i].model.transform.rotation);
                model.transform.parent = this.transform;
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

    public void ReturnToPage()
    {
        SceneManager.LoadScene("ListPage");
    }
}
