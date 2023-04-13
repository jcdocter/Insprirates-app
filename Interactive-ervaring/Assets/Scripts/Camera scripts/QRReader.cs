using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;

public class QRReader : MonoBehaviour
{
    public RawImage background;
    public AspectRatioFitter fit;
    public RectTransform scannerTransform;
    public GameObject scanner;

    private WebCamTexture backCam;
    private bool camAvailable;
    private QuestHandler questHandler;

    private void Start()
    {
        scanner.SetActive(false);
        questHandler = FindObjectOfType<QuestHandler>();
        StartCamera();
    }

    private void Update()
    {
        if(camAvailable == false)
        {
            return;
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        Scan();
    }

    private void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if(!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, (int)scannerTransform.rect.width, (int)scannerTransform.rect.height);
            }
        }

        backCam.Play();
        background.texture = backCam;
        camAvailable = true;
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(backCam.GetPixels32(), backCam.width, backCam.height);

            if(result != null)
            {
                questHandler.AddNewQuest(result.Text);
                scanner.SetActive(false);
            }
        }
        catch
        {
            Debug.LogError("Can not scan QR");
        }
    }

    public void ActivateCamera()
    {
        Tutorial.questTutorial.enabled = false;
        Tutorial.telescopeTutorial.enabled = false;
        Tutorial.scanTutorial.enabled = true;

        scanner.SetActive(true);
    }
}
