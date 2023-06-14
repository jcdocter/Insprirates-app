using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Maskman
{
    public int ID;
    public GameObject maskmanObject;
}

public class Raft : MonoBehaviour
{
    public List<Maskman> maskmanList = new List<Maskman>();
    private bool canShowRaft;

    private void Start()
    {
        for (int i = 0; i < maskmanList.Count; i++)
        {
            if (SavedID(maskmanList[i].ID))
            {
                maskmanList[i].maskmanObject.SetActive(true);
                canShowRaft = true;
            }
            else
            {
                maskmanList[i].maskmanObject.SetActive(false);
            }
        }

        if(!canShowRaft)
        {
            this.gameObject.SetActive(false);
        }
    }

    private bool SavedID(int _id)
    {
        SaveSystem.LoadQuest();

        for (int i = 0; i < SaveSystem.checkedID.Length; i++)
        {
            if(_id == SaveSystem.checkedID[i])
            {
                return true;
            }
        }

        return false;
    }
}

/*[System.Serializable]
public class RaftData
{
    public int[] checkedID = new int[5];

    public RaftData(int _ids)
    {
        for (int i = 0; i < checkedID.Length; i++)
        {
            if(checkedID[i] == 0)
            {
                this.checkedID[i] = _ids;
                return;
            }
        }
    }
}*/
