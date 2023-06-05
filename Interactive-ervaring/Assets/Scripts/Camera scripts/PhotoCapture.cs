using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public bool tookPhoto = false;

    private RecCamera recCam;
    private Image photoDisplayArea;
    private Button photoButton;
    private Texture2D screenCapture;

    private void Start()
    {
        recCam = FindObjectOfType<RecCamera>();
    }

    private void Update()
    {
        if(!this.gameObject.activeSelf)
        {
            recCam.canSwitchCam = false;
        }
        else
        {
            recCam.canSwitchCam = true;
        }
    }

    public void TakePicture()
    {
        photoButton.gameObject.SetActive(false);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        StartCoroutine(CapturePhoto());

        tookPhoto = true;
    }

    public IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0.0f, 0.0f, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        ShowPhoto();
        SavePhoto();
    }

    private void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);

        photoDisplayArea.sprite = photoSprite;
        photoDisplayArea.enabled = true;
    }

    private void SavePhoto()
    {
        var bytes = screenCapture.EncodeToPNG();

        var folder = Directory.CreateDirectory(Application.persistentDataPath + "/Treasure-map-pieces/");

        if(!Debugger.OnDevice())
        {
            PlayerPrefs.SetInt("questID", 1);
        }

        File.WriteAllBytes(folder + "TreasurePiece_" + PlayerPrefs.GetInt("questID") + ".png", bytes);
    }
}