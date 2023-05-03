using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDone
{
    string questSceneProperty { get; set; }
    void CheckOffQuest();
}
