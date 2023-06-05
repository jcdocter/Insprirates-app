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
    public GameObject piece;
}

public class Map : MonoBehaviour
{
    public LayerMask clickableLayer;
    public List<MapPieces> piecesList = new List<MapPieces>();

    private RawImage photo;
    private MapPieces mapPiece;
    private bool hitPiece;

    private void Start()
    {
        photo = FindObjectOfType<RawImage>();

        foreach (MapPieces piece in piecesList)
        {
            piece.piece.SetActive(PhotoExist(piece.photoID));
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
                ActivateMap(true);
                photo.texture = null;
                hitPiece = false;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    private void HitObject()
    {
        if((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Debugger.OnDevice() ? Input.touches[0].position : Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                foreach (MapPieces piece in piecesList)
                {
                    if (hit.transform.name == piece.piece.gameObject.name)
                    {
                        hitPiece = true;
;                       mapPiece = piece;
                    }
                }

                OpenPhoto();
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

    private void OpenPhoto()
    {
        ActivateMap(false);

        Byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/Treasure-map-pieces/" + "TreasurePiece_" + mapPiece.photoID + ".png");

        Texture2D displayPhoto = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        displayPhoto.LoadImage(bytes);

        if (displayPhoto != null)
        {
            photo.texture = displayPhoto;
        }
    }

    private bool PhotoExist(int _id)
    {
        string path = Application.persistentDataPath + "/Treasure-map-pieces/" + "TreasurePiece_" + _id + ".png";

        if (File.Exists(path))
        {
            return true;
        }

        return false;
    }

    private void ActivateMap(bool _activate)
    {
        foreach (MapPieces piece in piecesList)
        {
            piece.piece.SetActive(_activate);
        }
    }
}
