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

    private float checkTimer = 5.0f;
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
        canSwitchCam = false;
    }

    protected override void Update()
    {
        base.Update();

        if (FoundQR())
        {
            Scan();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            AcceptQuest();
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

    private bool FoundQR()
    {
        checkTimer -= Time.deltaTime;

        if(checkTimer <= 0)
        {
            checkTimer = 5.0f;

            int camWidth = backCam.width / 8;   // 640 / 8 = 80
            int camHeight = backCam.height / 8; // 480 / 8 = 60

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

                   Debug.Log($"Width: {j}, Height: {i}");

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
