using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconData : MonoBehaviour
{
    public int id;

    public void ChooseIcon()
    {
        Confirm.iconId = id;
        Confirm.choseIcon = true;
    }
}
