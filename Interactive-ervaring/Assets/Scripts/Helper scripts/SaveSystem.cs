using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static List<Quest> questList = new List<Quest>();
    public static int[] checkedID = new int[5];

    private const string subProfileFile = "/saveProfileFile.isr";
    private const string subInventoryFile = "/saveInventoryFile.isr";
    private const string subQuestFile = "/saveQuestFile.irs";
    private const string subQuestCountFile = "/saveQuestFile.count.isr";

    public static void SavePlayer(Confirm _data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + subProfileFile;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_data);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + subProfileFile;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file is not found in " + path);
            return null;
        }
    }

    public static void SaveQuest()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + subQuestFile;
        string countPath = Application.persistentDataPath + subQuestCountFile;
        string inventoryPath = Application.persistentDataPath + subInventoryFile;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, questList.Count);
        countStream.Close();

        FileStream inventoryStream = new FileStream(inventoryPath, FileMode.Create);
        InventoryData inventoryData = new InventoryData(Inventory.GetInstance());
        formatter.Serialize(inventoryStream, inventoryData);
        inventoryStream.Close();

        for (int i = 0; i < questList.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            QuestData data = new QuestData(questList[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static void LoadQuest()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + subQuestFile;
        string countPath = Application.persistentDataPath + subQuestCountFile;
        string inventoryPath = Application.persistentDataPath + subInventoryFile;

        int questCount = 0;

        if(File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            questCount = (int)formatter.Deserialize(countStream);
            countStream.Close();

            FileStream inventoryStream = new FileStream(inventoryPath, FileMode.Open);
            InventoryData inventoryData = formatter.Deserialize(inventoryStream) as InventoryData;
            Inventory.GetInstance().amountOfFish = inventoryData.amountOfFish;
            Inventory.GetInstance().amountOfCrownPieces = inventoryData.amountOfCrownPieces;
            Inventory.GetInstance().amountOfRecruits = inventoryData.amountOfRecruits;
            inventoryStream.Close();
        }
        else
        {
            Debug.LogError("Save file is not found in " + path);
        }

        int IDIndex = 0;

        for (int i = 0; i < questCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                QuestData data = formatter.Deserialize(stream) as QuestData;
                questList[i].isDone = data.isDone;

                if (questList[i].isDone && questList[i].hasRecruits)
                {
                    checkedID[IDIndex] = data.questID;
                    IDIndex++;
                }

                stream.Close();
            }
            else
            {
                Debug.LogError("Save file is not found in " + path);
            }
        }
    }
}
