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

    public RawImage photo;
    private MapPieces mapPiece;
    private bool canRotate;
    private bool hitPiece;
//    private int tapCount;

    private void Start()
    {
        photo = FindObjectOfType<RawImage>();
    }

    private void Update()
    {
       // RotateMap();
        HitObject();
        //        ZoomInOut();

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

    private void RotateMap()
    {
        if(!canRotate)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                transform.Rotate(screenTouch.deltaPosition.y, 0.0f, 0.0f);
               // transform.Rotate(0.0f, 0.0f, -screenTouch.deltaPosition.x);
            }

            if(screenTouch.phase == TouchPhase.Ended)
            {
                canRotate = false;

                if(hitPiece)
                {
//                    tapCount++;
//                    StartCoroutine(OpenPhoto());
                    hitPiece = false;
                }

            }
        }
    }

    private void HitObject()
    {
        if((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Debugger.OnDevice() ? Input.touches[0].position : Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.name == this.gameObject.name)
                {
                    canRotate = !canRotate;
                }
            }

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
        //        yield return new WaitForSeconds(0.3f);

        ActivateMap(false);

        Byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/Treasure-map-pieces/" + "TreasurePiece_" + mapPiece.photoID + ".png");

        Texture2D displayPhoto = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        displayPhoto.LoadImage(bytes);

        if (displayPhoto != null)
        {
            photo.texture = displayPhoto;
        }
    }

    private void ActivateMap(bool _activate)
    {
        foreach (MapPieces piece in piecesList)
        {
            piece.piece.SetActive(_activate);
        }
    }
}
