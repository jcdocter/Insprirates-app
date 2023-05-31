using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QuestScanner : RecCamera
{
    public GameObject acceptTutorial;
    public GameObject acceptButton;

    private List<Quest> questList = new List<Quest>();

    private float checkTimer = 5.0f;
    private string resultText;
    public int questID;

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

/*        if (FoundQR())
        {
            Scan();
        }*/

        if(!Debugger.OnDevice())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AcceptQuest();
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
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

                if(ActivateButton(quest))
                {
                    return;
                }
            }

        }
        catch
        {
            Debug.LogError("Can not scan QR");
            return;
        }
    }

    private bool FoundQR()
    {
        checkTimer -= Time.deltaTime;

        if(checkTimer <= 0)
        {
            checkTimer = 5.0f;

            int camWidth = backCam.width / 24;   // 640 / 8 = 80
            int camHeight = backCam.height / 24; // 480 / 8 = 60

            int startWidth = (backCam.width - camWidth) / 2;   // (640 - camWidth) / 2 = 280
            int startHeight = (backCam.height - camHeight) / 2; // (480 - camHeight) / 2 = 210

            Color32[] colors = backCam.GetPixels32();

            bool hasBlack = false;
            bool hasWhite = false;

            //needs to be lighter
            for (int i = startHeight; i < startHeight + camHeight; i++)
            {
                for (int j = startWidth; j < startWidth + camWidth; j++)
                {
                     //Color32 color = colors[j + i * (startWidth + camWidth)];
                     Color32 color = colors[j + i];

/*                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(j, i, 0.0f);
                    cube.transform.localScale = new Vector3(, i, 0.0f);*/

                    int R = color.r;
                    int G = color.g;
                    int B = color.b;

                   Debug.Log($"{R}, {G}, {B}");

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
                }
            }

            if (hasWhite && hasBlack)
            {
                return true;
            }
        }

        return false;
    }

    public bool ActivateButton(Quest _quest)
    {
        foreach (QRID qr in _quest.qrList)
        {
            Debugger.WriteData($"{qr.id} == {resultText} && {qr.activeQR}"); 
            if (qr.id == resultText && qr.activeQR)
            {
                acceptButton.SetActive(true);
                acceptTutorial.SetActive(true);

                PlayerPrefs.SetString("qrID", qr.id);
                questID = _quest.ID;

                acceptButton.GetComponent<Image>().color = new Color(181f / 255f, 249f / 255f, 249f / 255f);

                return true;
            }
        }

        return false;
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetInt("questID", questID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
