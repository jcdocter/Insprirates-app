using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmObject : MonoBehaviour
{
    private PhotoCapture photoCapture;
    private Rules rules = new Rules();
    private Swipe swipe = new Swipe();

    private void Start()
    {
        photoCapture = FindObjectOfType<PhotoCapture>();
        photoCapture.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!Debugger.OnDevice())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Inventory.GetInstance().amountOfRecruits++;

                rules.CheckOffQuest();
            }
        }

/*        if(photoCapture.gameObject.activeSelf)
        {
            if(rules.photoCapture.tookPhoto)
            {
                rules.CheckOffQuest();
            }
        }*/

        Swipe();
    }

    private void Swipe()
    {
        if(swipe.CheckSwipe())
        {
            Inventory.GetInstance().amountOfRecruits++;

/*            if(Inventory.GetInstance().amountOfRecruits == 5)
            {
                photoCapture.gameObject.SetActive(true);
                rules.ShowReward(FindObjectOfType<ObjectSpawner>().transform);
            }*/

            rules.CheckOffQuest();
        }
    }
}
