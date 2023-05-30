using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconData : MonoBehaviour
{
    public int id;
    public GameObject outline;

    private Confirm confirm;

    private void Start()
    {
        confirm = FindObjectOfType<Confirm>();
        outline.SetActive(false);
    }

    private void Update()
    {
        if(confirm.iconId != id)
        {
            outline.SetActive(false);
        }
    }

    public void ChooseIcon()
    {
        confirm.iconId = id;
        confirm.choseIcon = true;
        outline.SetActive(true);
    }
}
