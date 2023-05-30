using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecCamera : MonoBehaviour
{
    [HideInInspector]
    public bool canSwitchCam = false;
    
    protected WebCamTexture backCam;
    protected AspectRatioFitter fit;
    protected bool camAvailable;

    private RawImage background;
    private RectTransform backgroundTransform;
    private Swipe swipe = new Swipe();
    private bool switchCam;

    protected virtual void Start()
    {
        fit = GetComponentInChildren<AspectRatioFitter>();
        background = GetComponentInChildren<RawImage>();
        backgroundTransform = background.rectTransform;

        StartCoroutine(StartCamera());
    }

    protected virtual void Update()
    {
        if (swipe.CheckSwipe() && canSwitchCam)
        {
            switchCam = !switchCam;
            StartCoroutine(StartCamera());
        }

        FitCamera();
    }

    protected void FitCamera()
    {
        if (!camAvailable)
        {
            return;
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    private IEnumerator StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            camAvailable = false;
            yield return new WaitForSeconds(0.0f);
        }

        if (!backgroundTransform.gameObject.activeSelf)
        {
            backgroundTransform = background.rectTransform;
        }

        string deviceName = switchCam ? devices[1].name : devices[0].name;

        backCam = new WebCamTexture(deviceName, (int)backgroundTransform.rect.width, (int)backgroundTransform.rect.height);

        backCam.Play();
        yield return new WaitForSeconds(5.0f);
        background.texture = backCam;
        camAvailable = true;
    }
}
