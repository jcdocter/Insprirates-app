using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QuestScanner : RecCamera
{
    public GameObject acceptTutorial;

    private List<Quest> questList = new List<Quest>();
    private GameObject acceptButton;

    private static string resultText;

    private void Awake()
    {
        acceptButton = FindObjectOfType<Button>().gameObject;
    }

    protected override void Start()
    {
        base.Start();

        questList = SaveSystem.questList;

        acceptButton.SetActive(false);
        acceptTutorial.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (!Debugger.OnDevice())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                resultText = "A1";
                PlayerPrefs.SetString("modelID", resultText);
                AcceptQuest();
            }
        }
        else
        {
            Scan();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(backCam.GetPixels32(), backCam.width, backCam.height);

            if (result == null)
            {
                return;
            }

            resultText = result.Text;

            ActivateButton();
        }
        catch
        {
            Debug.LogError("Can not scan QR");
            return;
        }
    }

    public void ActivateButton()
    {
        foreach(Quest quest in questList)
        {
            foreach (QRID qr in quest.qrList)
            {
                if(qr.id == resultText && qr.activeQR)
                {
                    acceptButton.SetActive(true);
                    acceptTutorial.SetActive(true);

                    acceptButton.GetComponent<Image>().color = new Color(181f / 255f, 249f / 255f, 249f / 255f);
                }
            }
        }
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetString("modelID", resultText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
