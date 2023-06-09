using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmObject : MonoBehaviour
{
    public bool canTap;

    private PhotoCapture photoCapture;
    private Rules rules = new Rules();
    private Swipe swipe = new Swipe();

    private Texture2D raftCapture;
    private Texture2D maskCapture;

    private void Start()
    {
        photoCapture = FindObjectOfType<PhotoCapture>();
        photoCapture.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(photoCapture.gameObject.activeSelf)
        {
            if(rules.photoCapture.tookPhoto)
            {
                rules.CheckOffQuest();
            }
        }

        if(canTap)
        {
            Tap();
        }
        else
        {
            Swipe();
        }
    }

    private void Tap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }

    private void Swipe()
    {
        if(swipe.CheckSwipe())
        {
            Inventory.GetInstance().amountOfRecruits++;

            if(Inventory.GetInstance().amountOfRecruits == 5)
            {
                photoCapture.gameObject.SetActive(true);
                rules.ShowReward(FindObjectOfType<ObjectSpawner>().transform);
            }
            else
            {
                TakeNewPhoto();
            }

            rules.CheckOffQuest();
        }
    }

    public void TakeNewPhoto()
    {
        maskCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        StartCoroutine(CapturePhoto());
    }

    private IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0.0f, 0.0f, Screen.width, Screen.height);

        maskCapture.ReadPixels(regionToRead, 0, 0, false);
        maskCapture.Apply();

        SavePhoto();
    }

    private void SavePhoto()
    {
        Byte[] maskByte = maskCapture.EncodeToPNG();
        string file = "TreasurePieceRaft.png";
        string path = Application.persistentDataPath + "/Treasure-map-pieces/" + file;

//        var folder = Directory.CreateDirectory(Application.persistentDataPath + "/Treasure-map-pieces/");
 //       File.WriteAllBytes(folder + file, bytes);
    }

/*    private Color32[] GetNewLayer()
    {
//      Color32[] colorRaft = screenCapture.GetPixels32();
        Color32[] colorMaskPeople = screenCapture.GetPixels32();

        for (int i = 0; i < colorMaskPeople.Length; i++)
        {
            colorRaft[i] = colorMaskPeople[i];
        }

        return colorRaft;
    }*/

    public void Unlocked()
    {
        rules.CheckOffQuest();
    }
}
