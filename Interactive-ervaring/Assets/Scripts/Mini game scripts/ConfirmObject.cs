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

/*            if(Inventory.GetInstance().amountOfRecruits == 5)
            {
                photoCapture.gameObject.SetActive(true);
                rules.ShowReward(FindObjectOfType<ObjectSpawner>().transform);
            }*/

            rules.CheckOffQuest();
        }
    }

    public void Unlocked()
    {
        rules.CheckOffQuest();
    }
}
