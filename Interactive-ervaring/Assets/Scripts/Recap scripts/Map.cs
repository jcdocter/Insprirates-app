using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Map : MonoBehaviour
{
    private bool canRotate;

    private void Update()
    {
        RotateMap();
        HitObject();
        ZoomInOut();
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
            }
        }
    }

    private void HitObject()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.name == this.gameObject.name)
                {
                    canRotate = !canRotate;
                }
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

            Zoom(difference * 0.1f);
        }
    }

    private void Zoom(float _increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - _increment, 1.0f, 50.0f);
    }
}
