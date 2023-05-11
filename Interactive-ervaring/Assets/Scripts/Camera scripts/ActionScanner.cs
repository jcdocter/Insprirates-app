using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZXing;

public class ActionScanner : ARecCamera
{
    public bool hasScanned = false;
    public GameObject scanner;

    private string resultText;

    protected override void Start()
    {
        base.Start();
        resultText = PlayerPrefs.GetString("modelID") + "-1";
    }

    protected override void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(backCam.GetPixels32(), backCam.width, backCam.height);

            if (resultText == result.Text)
            {
                hasScanned = true;
            }
        }
        catch
        {
            Debug.LogError("Can not scan QR");
        }
    }
}
