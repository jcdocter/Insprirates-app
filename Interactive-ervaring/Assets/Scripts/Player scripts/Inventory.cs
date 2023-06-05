using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public static Inventory instance;
    public int amountOfFish;
    public int amountOfCrownPieces;
    public int amountOfRecruits;

    public static Inventory GetInstance()
    {
        if (instance == null)
        {
            instance = new Inventory();
        }
        return instance;
    }
}

[System.Serializable]
public class InventoryData
{
    public int amountOfFish;
    public int amountOfCrownPieces;
    public int amountOfRecruits;

    public InventoryData(Inventory _data)
    {
        this.amountOfFish = _data.amountOfFish;
        this.amountOfCrownPieces = _data.amountOfCrownPieces;
        this.amountOfRecruits = _data.amountOfRecruits;
    }
}
