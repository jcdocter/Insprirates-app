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

    private string resultText;
    public int questID;

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
            if(Input.GetKey(KeyCode.Space))
            {
                AcceptQuest();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Scan();
            }
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

            foreach (Quest quest in questList)
            {
              ActivateButton(quest);
            }

        }
        catch
        {
            Debug.LogError("Can not scan QR");
            return;
        }
    }

    public void ActivateButton(Quest _quest)
    {
        foreach (QRID qr in _quest.qrList)
        {
            if(qr.id == resultText && qr.activeQR)
            {
                acceptButton.SetActive(true);
                acceptTutorial.SetActive(true);

                questID = _quest.ID;

                acceptButton.GetComponent<Image>().color = new Color(181f / 255f, 249f / 255f, 249f / 255f);
            }
        }
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetInt("questID", questID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
