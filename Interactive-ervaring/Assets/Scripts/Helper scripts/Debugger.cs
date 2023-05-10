using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static TextMeshProUGUI debugData;

    private void Start()
    {
        debugData = FindObjectOfType<TextMeshProUGUI>();
    }

    public static void WriteData(string _text)
    {
        debugData.text = _text;
    }
}
