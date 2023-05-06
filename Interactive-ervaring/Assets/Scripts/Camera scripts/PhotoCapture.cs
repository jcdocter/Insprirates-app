using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture
{
    private Image photoDisplayArea;
    private Texture2D screenCapture;

    public void SetScreenCapture()
    {;
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    public IEnumerator CapturePhoto(Image _photoDisplayArea)
    {
        yield return new WaitForEndOfFrame();

        this.photoDisplayArea = _photoDisplayArea;

        Rect regionToRead = new Rect(0.0f, 0.0f, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    private void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoDisplayArea.enabled = true;
    }

    public Image GetPhoto() 
    {
        return photoDisplayArea;
    }
}