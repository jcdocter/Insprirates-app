using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class MapPieces
{
    public int photoID;
    public RawImage image;
}

public class Map : MonoBehaviour
{
    public LayerMask clickableLayer;
    public RawImage photo;
    public List<MapPieces> piecesList = new List<MapPieces>();

    private bool hitPiece;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        foreach (MapPieces piece in piecesList)
        {
            piece.image.texture = PhotoExist(piece.photoID, (int)piece.image.rectTransform.rect.width, (int)piece.image.rectTransform.rect.height);
        }
    }

    private void Update()
    {
        HitObject();
        ZoomInOut();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (hitPiece)
            {
                photo.texture = null;
                hitPiece = false;

                for (int i = 0; i < transform.childCount; ++i)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                Screen.orientation = ScreenOrientation.Portrait;
                SceneManager.LoadScene("ListPage");
            }
        }
    }

    private void HitObject()
    {
        if((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Debugger.OnDevice() ? Input.touches[0].position : Input.mousePosition);

            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                return;
            }

            foreach (MapPieces piece in piecesList)
            {
                if (hit.transform.name != piece.image.name || PhotoExist(piece.photoID, Screen.width, Screen.height) == null)
                {
                    continue;
                }

                hitPiece = true;
                for (int i = 0; i < transform.childCount; ++i)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }

                photo.texture = PhotoExist(piece.photoID, Screen.width, Screen.height);
            }
        }
    }

    private void ZoomInOut()
    {
        if(Input.touchCount == 2)
        {
            Touch touchFirstHand = Input.GetTouch(0);
            Touch touchSecondHand = Input.GetTouch(1);

            Vector2 touchFirstPrevPosition = touchFirstHand.position - touchFirstHand.deltaPosition;
            Vector2 touchSecondPrevPosition = touchSecondHand.position - touchSecondHand.deltaPosition;

            float prevMagnitude = (touchFirstPrevPosition - touchSecondPrevPosition).magnitude;
            float currentMagnitude = (touchFirstHand.position - touchSecondHand.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
    }

    private void Zoom(float _increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - _increment, 1.0f, 25.0f);
    }

    private Texture2D PhotoExist(int _id, int _width, int _height)
    {
        string path = Application.persistentDataPath + "/Treasure-map-pieces/" + "TreasurePiece_" + _id + ".png";

        if (File.Exists(path))
        {
            Byte[] bytes = File.ReadAllBytes(path);

            Texture2D displayPhoto = new Texture2D(_width, _height, TextureFormat.RGB24, false);
            displayPhoto.LoadImage(bytes);

            return displayPhoto;
        }

        return null;
    }
}
