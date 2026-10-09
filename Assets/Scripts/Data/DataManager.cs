using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public GameData gameData;
    private string dataFilePath;

    private void Awake()
    {
        dataFilePath = Application.persistentDataPath + "/gameData.json";
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(gameData);

        //Write JSON string to file
        File.WriteAllText(dataFilePath, jsonData);

        Debug.Log("Data saved to: " + dataFilePath);
    }

    public void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            //Read JSON strng from file
            string jsonData = File.ReadAllText(dataFilePath);

            //Convert JSON string to gameData object
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
    }
}
