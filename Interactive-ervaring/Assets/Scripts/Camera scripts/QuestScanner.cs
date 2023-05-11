using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QuestScanner : ARecCamera
{
    public GameObject scanner;
    public GameObject acceptButton;
    public GameObject acceptTutorial;

    private List<Quest> questList = new List<Quest>();

    private static string resultText;

    protected override void Start()
    {
        base.Start();

        questList = SaveSystem.questList;

        acceptButton.SetActive(false);
        acceptTutorial.SetActive(false);
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
                    if (questList[i].id == resultText && questList[i].startQuest)
                    {
                        acceptButton.SetActive(true);
                        acceptTutorial.SetActive(true);

                        acceptButton.GetComponent<Image>().color = DisplayButtonColor(i);
                    }
                }
            }
        }
        catch
        {
            Debug.LogError("Can not scan QR");
        }
    }

    private Color DisplayButtonColor(int _index)
    {
        if (questList[_index].isStory)
        {
            return new Color(255f/255f, 212f/255f, 180f/255f);
        }
        else
        {
            return new Color(181f/255f, 249f/255f, 249f/255f);
        }
    }

    public void AcceptQuest()
    {
        PlayerPrefs.SetString("questID", resultText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
