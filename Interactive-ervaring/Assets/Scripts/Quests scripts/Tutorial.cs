using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject questTutorial;

    public GameObject telescopeTutorial;
    public GameObject firstQuestTutorial;
    public GameObject scanTutorial;
    public GameObject acceptTutorial;

    public void Awake()
    {
        instance = this;
    }

}
