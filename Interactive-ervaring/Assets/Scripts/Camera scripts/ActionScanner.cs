using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class ActionScanner : ARecCamera
{
    public bool hasScanned = false;
    public GameObject scanner;

    public Image photoDisplayArea;

    private PhotoCapture photoCapture = new PhotoCapture();
    private string resultText;

    protected override void Start()
    {
        photoDisplayArea.enabled = false;

        base.Start();
        resultText = PlayerPrefs.GetString("modelID") + "-1";

        photoCapture.SetScreenCapture();
    }

    private void Update()
    {
        FitCamera();
        Scan();
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

    public void TakePicture()
    {
        StartCoroutine(photoCapture.CapturePhoto(photoDisplayArea));

        photoDisplayArea = photoCapture.GetPhoto();
    }
}
