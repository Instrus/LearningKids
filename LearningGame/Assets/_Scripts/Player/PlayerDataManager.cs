using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerDataManager : MonoBehaviour
{

    // Note: PlayerData and Manager are saving the wrong data. All player data is being saved but we need only the
    // current players data to be saved. We can always save just the players data, and add it to the list,
    // then sort the data.

    PlayerData playerData; // Players data
    private string jsonFilePath; // Path to scoreboard JSON file

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        // Set the relative path to the scoreboard in the persistent data path
        jsonFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");

        // Load the initial scoreboard data from Resources if it doesn't exist in the persistent data path
        if (!File.Exists(jsonFilePath)) { LoadInitialScoreboardData(); }

        // Load the player data from the persistent data path
        LoadPlayerDataFromFile(); 
    }

    // Save the player's score to the persistent JSON file
    public void SavePlayerDataToFile()
    {
        PlayerList playerList = LoadScoreboardFromFile(); // get list of players
        Player player = playerList.players.Find(p => p.username == playerData.GetUsername()); // search for player within file by username

        if (player != null) // if player found, set score and currency
        {
            player.score = playerData.GetScore();
            player.currency = playerData.GetCurrency();
        }
        else // else add the new player to the file
        {
            playerList.players.Add(new Player { username = playerData.GetUsername(), score = playerData.GetScore() });
        }
            
        // Sort the players based on their scores (ascending order)
        playerList.players.Sort((a, b) => a.score.CompareTo(b.score));

        // saves data to file
        string persistentScoreboardFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        string json = JsonUtility.ToJson(playerList);
        File.WriteAllText(persistentScoreboardFilePath, json); // overwrite the data to the file
    }

    // Load the persistent JSON file into memory
    public PlayerList LoadScoreboardFromFile() // function is only ever called from Save
    {
        string persistentScoreboardFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");

        if (File.Exists(persistentScoreboardFilePath))
        {
            string json = File.ReadAllText(persistentScoreboardFilePath);
            return JsonUtility.FromJson<PlayerList>(json); // returns list of players from file
        }
        else
        {
            Debug.LogWarning("Scoreboard file not found. path: " + persistentScoreboardFilePath);
            return new PlayerList { players = new System.Collections.Generic.List<Player>() };
        }
    }

    // Load the player's score from the JSON file into memory
    public void LoadPlayerDataFromFile()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath); // read the data from file
            PlayerList playerList = JsonUtility.FromJson<PlayerList>(json); // get a list of every player within the file

            Player player = playerList.players.Find(p => p.username == playerData.GetUsername()); // search for player within file by username

            if (player != null) //if found, set score and currency
            {
                playerData.SetScore(player.score);
                playerData.SetCurrency(player.currency);
            }

        }
    }

    // Load the scoreboard from the non-persistent writable JSON file
    public void LoadInitialScoreboardData()
    {
        string initialScoreboardFilePath = "scoreboard";  // Assumes the file is named "scoreboard.json" in the Resources folder
        string json = Resources.Load<TextAsset>(initialScoreboardFilePath).text;
        File.WriteAllText(jsonFilePath, json);
    }

}
