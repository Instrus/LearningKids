using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData; // Holds the player's data
    private string jsonFilePath; // Path to scoreboard JSON file

    private void Awake()
    {
        // Ensure playerData is always set
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        // Define the persistent file path for the player data
        jsonFilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        if (!File.Exists(jsonFilePath))
        {
            // Create a persistent file for new users. Use the default PlayerData values.
            SavePlayerDataToFile();
        }
        else
        {
            // Persistent file exists. Load the player data into memory
            LoadPlayerDataFromFile();
        }
    }

    // Save the updated player data to the persistent JSON file
    public void SavePlayerDataToFile()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(jsonFilePath, json);
    }

    // Load the player data from the persistent JSON file into memory
    public void LoadPlayerDataFromFile()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            // Overwrite the playerData object with the JSON data
            JsonUtility.FromJsonOverwrite(json, playerData);
        }
        else
        {
            Debug.Log("Can't find PlayerData.json. Using the default PlayerData values.");
        }
    }
}
