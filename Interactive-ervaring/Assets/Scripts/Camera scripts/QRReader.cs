using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QRReader : RecCamera
{
    public GameObject scanner;
    public GameObject acceptButton;
    public GameObject acceptTutorial;

    private List<Quest> questList = new List<Quest>();

    private static string resultText;

    public override void Start()
    {
        base.Start();

        questList = SaveSystem.questList;

        acceptButton.SetActive(false);
        acceptTutorial.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        Scan();
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
