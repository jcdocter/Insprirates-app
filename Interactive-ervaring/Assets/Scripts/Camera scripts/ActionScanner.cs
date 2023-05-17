using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class ActionScanner : ARecCamera
{
    public Image photoDisplayArea;

    private PhotoCapture photoCapture = new PhotoCapture();

    protected override void Start()
    {
        photoDisplayArea.enabled = false;

        base.Start();
    }

    private void Update()
    {
        FitCamera();
    }

    public void TakePicture()
    {
        Rules.photoButton.SetActive(false);
        photoCapture.SetScreenCapture();
        StartCoroutine(photoCapture.CapturePhoto(photoDisplayArea));

        photoDisplayArea = photoCapture.GetPhoto();
    }
}
