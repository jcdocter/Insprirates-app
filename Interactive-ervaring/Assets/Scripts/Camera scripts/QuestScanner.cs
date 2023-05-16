using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QuestScanner : ARecCamera
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

    private void Update()
    {
        FitCamera();
        Scan();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    protected override void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(backCam.GetPixels32(), backCam.width, backCam.height);

            if(result != null)
            {
                resultText = result.Text;

                for (int i = 0; i < questList.Count; i++)
                {
                    if (questList[i].id == resultText )
                    {
                        if(!questList[i].startQuest || questList[i].isDone)
                        {
                            return;
                        }

                        acceptButton.SetActive(true);
                        acceptTutorial.SetActive(true);

                        acceptButton.GetComponent<Image>().color = new Color(181f / 255f, 249f / 255f, 249f / 255f);
                    }
                }
            }
        }
        catch
        {
            Debug.LogError("Can not scan QR");
        }
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetString("modelID", resultText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
