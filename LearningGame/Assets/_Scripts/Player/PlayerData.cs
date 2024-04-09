using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour // Keep as mono because of awake function
{
    // Path to scoreboard JSON file
    private string jsonFilePath;

    // Singleton
    public static PlayerData instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        // Set the relative path to the scoreboard in the persistent data path
        jsonFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");

        // Load the initial scoreboard data from Resources if it doesn't exist in the persistent data path
        if (!File.Exists(jsonFilePath))
        {
            LoadInitialScoreboardData();
        }

        // Load the player data from the persistent data path
        LoadPlayerDataFromFile();
    }

    // Holds data of player. Stored on local device. (work on save after)
    // Change these to be null (or equivalent) at first later.
    // PlayerID
    private int playerID = 1;
    // Username
    private string username = "Blueberry";
    // PIN
    private int pin = 1234;
    // Score
    [SerializeField] private int score = 0;
    // Currency
    [SerializeField] private int currency = 0;

    // potential:
    // Phone number
    // Home address

    // Function to compare PIN with pin screen input

    // Require getters/setters for each item. All need to be private.

    // get playerID
    public int getPlayerID()
    {
        return playerID;
    }
    // set playerID
    public void setPlayerID(int playerID)
    {
        this.playerID = playerID;
    }

    // get username
    public string getUsername()
    {
        return username;
    }
    // set username 
    public void setUsername(string userName)
    {
        this.username = userName;
    }

    // get pin
    public int getPin()
    {
        return pin;
    }

    // set pin
    public void setPin(int pin) {
        this.pin = pin;
    }

    // verify pin?

    // get score
    public int getScore()
    {
        return score;
    }
    // set score (do not call this, call increment score)
    public void setScore(int score)
    {
        this.score = score;

        // Save the score locally
        SavePlayerDataToFile();
    }

    // increments the score (verify through testing)
    public void incrementScore(int score)
    {
        int currentScore = getScore();
        int newScore = currentScore + score;
        setScore(newScore);
        
    }

    // get currency
    public int getCurrency()
    {
        return currency;
    }
    // set currency (do not call this, call incrmenet currency)
    public void setCurrency(int currency)
    {
        this.currency = currency;
    }
    
    public void incrementCurrency(int currency)
    {
        int currentCurrency = getCurrency();
        int newCurrency = currentCurrency + currency;
        setCurrency(newCurrency);
    }

    // FILE SAVE/LOAD FUNCTIONS ================================================

    // Save the player's score to the persistent JSON file
    private void SavePlayerDataToFile()
    {
        PlayerList playerList = LoadScoreboardFromFile();

        Player player = playerList.players.Find(p => p.username == username);

        if (player != null)
        {
            player.score = score;
        }
        else
        {
            playerList.players.Add(new Player { username = username, score = score });
        }

        // Sort the players based on their scores (ascending order)
        playerList.players.Sort((a, b) => a.score.CompareTo(b.score));

        string persistentScoreboardFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        string json = JsonUtility.ToJson(playerList);
        File.WriteAllText(persistentScoreboardFilePath, json);
    }

    // Load the persistent JSON file into memory
    private PlayerList LoadScoreboardFromFile()
    {
        string persistentScoreboardFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        if (File.Exists(persistentScoreboardFilePath))
        {
            string json = File.ReadAllText(persistentScoreboardFilePath);
            return JsonUtility.FromJson<PlayerList>(json);
        }
        else
        {
            Debug.LogWarning("Scoreboard file not found. path: " + persistentScoreboardFilePath);
            return new PlayerList { players = new System.Collections.Generic.List<Player>() };
        }
    }

    // Load the player's score from the JSON file into memory
    private void LoadPlayerDataFromFile()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            PlayerList playerList = JsonUtility.FromJson<PlayerList>(json);

            Player player = playerList.players.Find(p => p.username == username);

            if (player != null)
            {
                score = player.score;
            }
        }
    }

    // Load the scoreboard from the non-persistent writable JSON file
    private void LoadInitialScoreboardData()
    {
        string initialScoreboardFilePath = "scoreboard";  // Assumes the file is named "scoreboard.json" in the Resources folder
        string json = Resources.Load<TextAsset>(initialScoreboardFilePath).text;
        File.WriteAllText(jsonFilePath, json);
    }
}
