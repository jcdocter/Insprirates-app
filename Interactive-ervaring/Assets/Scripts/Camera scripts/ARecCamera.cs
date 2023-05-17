using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARecCamera : MonoBehaviour
{
    public AspectRatioFitter fit;
    public RawImage background;
    public RectTransform backgroundTransform;
    protected WebCamTexture backCam;
    protected bool camAvailable;

    protected virtual void Start()
    {
        StartCoroutine(StartCamera());
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

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                if(!backgroundTransform.gameObject.activeSelf)
                {
                    backgroundTransform = background.rectTransform;
                }

                backCam = new WebCamTexture(devices[i].name, (int)backgroundTransform.rect.width, (int)backgroundTransform.rect.height);
            }
        }

        backCam.Play();
        yield return new WaitForSeconds(5.0f);
        background.texture = backCam;
        camAvailable = true;
    }
}
