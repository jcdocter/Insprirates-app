using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DirectoryReader
{ 
    public static bool DirectoryExist()
    {
        try
        {
            return Directory.Exists(Application.persistentDataPath + "/Treasure-map-pieces");
        }
        catch
        {
            return false;
        }
    }
}
