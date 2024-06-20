using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerDataManager playerDM;

    // Should remove all the default values. Create a new PlayerData and assign them default values within PDM.

    // Holds data of player. Stored on local device.
    private int playerID = 1; // Default testing PlayerID
    private string username = "Blueberry"; // Default testing username
    private int pin = 1234; // Default testing player PIN
    private int score = 0; // initial player score
    private int currency = 0; // initial player currency
    // Avatar cosmetic indices
    private int avatarIndex = 0;
    private int hatIndex = 0;
    private int shirtIndex = 0;
    // potential: (Phone number / Home address)

    // Player ID
    public int GetPlayerID() { return playerID; }
    public void SetPlayerID(int playerID) { this.playerID = playerID; }

    // Player username
    public string GetUsername() { return username; }
    public void SetUsername(string userName) { this.username = userName; }

    // Player PIN
    public int GetPin() { return pin; }
    public void SetPIN(int pin) { this.pin = pin; }

    // Player currency
    public int GetCurrency() { return currency; }
    public void SetCurrency(int currency) //Do not call. Call AddCurrency
    { this.currency = currency; playerDM.SavePlayerDataToFile(); }  // Save the currency locally
    public void AddCurrency(int currency) { SetCurrency(GetCurrency() + currency); } 

    // Scores
    public int GetScore() { return score; }
    public void SetScore(int score) //Do not call. Call AddScore
    { this.score = score; playerDM.SavePlayerDataToFile(); } // Save the score locally
    public void AddScore(int score) { SetScore(GetScore() + score); }

    // Avatar cosmetics
    public int[] GetAvatar() { return new int[] { avatarIndex, hatIndex, shirtIndex }; }
    public void SetAvatar(int avatarIndex, int hatIndex, int shirtIndex)
    {
        this.avatarIndex = avatarIndex;
        this.hatIndex = hatIndex;
        this.shirtIndex = shirtIndex;
    }
    
}
