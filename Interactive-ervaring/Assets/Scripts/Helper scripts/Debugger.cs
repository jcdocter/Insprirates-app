using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class Debugger
{
    public static TextMeshProUGUI debugData;

    public static bool OnDevice()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            return true;
        }

        return false;
    }

    public static void WriteData(string _text)
    {
        debugData = GameObject.FindGameObjectWithTag("Debug").GetComponent<TextMeshProUGUI>();

        debugData.text = _text;
    }
}
