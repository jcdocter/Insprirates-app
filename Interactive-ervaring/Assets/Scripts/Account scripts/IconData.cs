using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconData : MonoBehaviour
{
    public int id;
    public GameObject outline;

    public void Start()
    {
        outline.SetActive(false);
    }

    public void Update()
    {
        if(Confirm.iconId != id)
        {
            outline.SetActive(false);
        }
    }

    public void ChooseIcon()
    {
        Confirm.iconId = id;
        Confirm.choseIcon = true;
        outline.SetActive(true);
    }
}
