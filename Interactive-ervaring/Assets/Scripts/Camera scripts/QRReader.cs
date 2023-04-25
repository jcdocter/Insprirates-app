using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QRReader : MonoBehaviour
{
    public RawImage background;
    public RectTransform scannerTransform;
    public GameObject scanner;
    public GameObject acceptButton;
    public GameObject acceptTutorial;

    private WebCamTexture backCam;
    private AspectRatioFitter fit;
    private List<Quest> questList = new List<Quest>();

    private bool camAvailable;
    private static string resultText;

    private void Start()
    {
        fit = FindObjectOfType<AspectRatioFitter>();

        questList = SaveSystem.questList;

        acceptButton.SetActive(false);
        acceptTutorial.SetActive(false);

        StartCoroutine(StartCamera());
    }

    private void Update()
    {
        if(!camAvailable)
        {
            return;
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        Scan();
    }

    private IEnumerator StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            camAvailable = false;
            yield return new WaitForSeconds(0.0f);
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if(!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, (int)scannerTransform.rect.width, (int)scannerTransform.rect.height);
            }
        }


        backCam.Play();
        yield return new WaitForSeconds(5.0f);
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
                resultText = result.Text;

                acceptButton.SetActive(true);
                acceptTutorial.SetActive(true);

                acceptButton.GetComponent<Image>().color = DisplayButtonColor();

            }
        }
        catch
        {
            Debug.LogError("Can not scan QR");
        }
    }

    private Color DisplayButtonColor()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id != resultText)
            {
                continue;
            }

            if (questList[i].isStory)
            {
                return new Color(255f/255f, 212f/255f, 180f/255f);
            }
            else
            {
                return new Color(181f/255f, 249f/255f, 249f/255f);
            }
        }

        return Color.white;
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetString("questID", resultText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
