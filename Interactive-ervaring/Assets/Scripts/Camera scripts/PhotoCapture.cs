using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public bool tookPhoto = false;

    private RecCamera recCam;
    private Button photoButton;
    private Texture2D screenCapture;

    private void Start()
    {
        recCam = FindObjectOfType<RecCamera>();
        photoButton = GetComponentInChildren<Button>();
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

        SavePhoto();
    }

    private void SavePhoto()
    {
        var bytes = screenCapture.EncodeToPNG();
        var folder = Directory.CreateDirectory(Application.persistentDataPath + "/Treasure-map-pieces/");

        File.WriteAllBytes(folder + "TreasurePiece_" + PlayerPrefs.GetInt("questID") + ".png", bytes);
    }
}