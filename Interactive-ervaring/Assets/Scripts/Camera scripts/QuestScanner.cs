using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using ZXing;

public class QuestScanner : RecCamera, IGoBack
{
    private List<Quest> questList = new List<Quest>();
    
    //Variables should be private.
    public string resultText;
    public int questID;

    protected override void Start()
    {
        base.Start();

        questList = SaveSystem.questList;
    }

    protected override void Update()
    {
        base.Update();

        if (!Debugger.OnDevice())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AcceptQuest();
            }
        }
    }

    public void ReturnToPage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Scan()
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
                if(quest.isDone)
                {
                    continue;
                }

                if(quest.QRID == resultText)
                {
                    questID = quest.ID;
                    AcceptQuest();
                    return;
                }

                Debugger.WriteData("Sorry no quest available");
            }

        }
        catch
        {
            Debug.LogError("Can not scan QR");
            return;
        }
    }

    // not needed for end product
    private bool FoundQR()
    {
        int camWidth = backCam.width / 16;   // 640 / 16 = 40
        int camHeight = backCam.height / 16; // 480 / 16 = 30

        int startWidth = (backCam.width - camWidth) / 2;   // (640 - camWidth) / 2 = 300
        int startHeight = (backCam.height - camHeight) / 2; // (480 - camHeight) / 2 = 225

        Color32[] colors = backCam.GetPixels32();

        bool hasBlack = false;
        bool hasWhite = false;
        int index = 0;

        //1200px are being checked
        for (int i = startWidth; i < startWidth + camWidth; i++)
        {
            for (int j = startHeight; j < startHeight + camHeight; j++)
            {
                Color32 color = colors[i + backCam.width * j];

                int R = color.r;
                int G = color.g;
                int B = color.b;

                if (color == Color.white)
                {
                    Debug.Log("White");
                    hasWhite = true;
                }

                if (color == Color.black)
                {
                    Debug.Log("Black");
                    hasWhite = true;
                }

                index++;
            }

            if (hasWhite && hasBlack)
            {
                return true;
            }
        }

        return false;
    }

    private void AcceptQuest()
    {
        PlayerPrefs.SetString("qrID", resultText);
        PlayerPrefs.SetInt("questID", questID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
