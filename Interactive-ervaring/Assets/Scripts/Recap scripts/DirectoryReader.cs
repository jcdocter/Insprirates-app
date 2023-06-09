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
            if(Inventory.GetInstance().amountOfRecruits > 0 || Directory.Exists(Application.persistentDataPath + "/Treasure-map-pieces"))
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
