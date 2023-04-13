using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Tutorial
{
    public static Image questTutorial;
    public static Image telescopeTutorial;
    public static Image scanTutorial;
    public static Image firstQuestTutorial;

    public static void assignTutorial(GameObject _tutorialObject)
    {
        questTutorial = _tutorialObject.transform.GetChild(0).GetComponent<Image>();
        telescopeTutorial = _tutorialObject.transform.GetChild(1).GetComponent<Image>();
        scanTutorial = _tutorialObject.transform.GetChild(2).GetComponent<Image>();
        firstQuestTutorial = _tutorialObject.transform.GetChild(3).GetComponent<Image>();
    }
}
