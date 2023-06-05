using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakKeyLock : MonoBehaviour
{
    public bool canTap;

    private GameObject photoCapture;
    private Rules rules = new Rules();
    private Swipe swipe = new Swipe();

    private void Start()
    {
        photoCapture = FindObjectOfType<PhotoCapture>().gameObject;
        photoCapture.SetActive(false);
    }

    private void Update()
    {
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
            rules.CheckOffQuest();
        }
    }

    public void Unlocked()
    {
        rules.CheckOffQuest();
    }
}
