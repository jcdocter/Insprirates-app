using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe
{
    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;

    public bool CheckSwipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if(endTouchPosition.y > startTouchPosition.y)
            {
                return true;
            }
        }

        return false;
    }
}
