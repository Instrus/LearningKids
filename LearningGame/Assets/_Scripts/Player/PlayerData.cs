using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour // Keep as mono because of awake function
{

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

    // Need a save/load function later.
}
