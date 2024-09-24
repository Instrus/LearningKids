using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerDataManager playerDM;

    // Default PlayerData members. Stored on local device.
    [SerializeField] private int playerID = 1;
    [SerializeField] private string username = "Blueberry";
    [SerializeField] private int pin = 1234;
    [SerializeField] private int score = 0;
    [SerializeField] private int currency = 0;
    // unlocked cosmetics are tracked here.  link between cosmetics database and loading to avatar (add addition method)
    public List<int> unlockedAvatarIDs= new List<int>(); // add to save
    public List<int> unlockedHatsIDs = new List<int>(); // add to save
    public List<int> unlockedClothesIDs = new List<int>(); // add to save
    // tracks current equipped avatar cosmetics
    [SerializeField] private int avatarIndex = 0;
    [SerializeField] private int hatIndex = 0;
    [SerializeField] private int shirtIndex = 0;
    [SerializeField] private List<ContactInfo> contacts = new List<ContactInfo>();

    // user settings
    [SerializeField] public int effectsVolume;
    [SerializeField] public int musicVolume; //getters and setters

    private void Awake() { 
        // ensures playerDM is always set
        playerDM = GameObject.Find("PlayerDataManager").GetComponent<PlayerDataManager>();
    }

    // temp - to populate / test avatar
    private void Start()
    {
        if (unlockedHatsIDs.Count == 0)
        {
            unlockedAvatarIDs.Add(0);
            unlockedAvatarIDs.Add(1);
            unlockedAvatarIDs.Add(2);
            unlockedAvatarIDs.Add(3);
        }
        if (unlockedHatsIDs.Count == 0)
        {
            unlockedHatsIDs.Add(0);
            unlockedHatsIDs.Add(1);
            unlockedHatsIDs.Add(2);
        }
        if (unlockedClothesIDs.Count == 0)
        {
            unlockedClothesIDs.Add(0);
            unlockedClothesIDs.Add(1);
            unlockedClothesIDs.Add(2);
        }
    }

    // Save all serialized player data to a persistent JSON file.
    public void Save() { 
        if (playerDM != null)
            playerDM.SavePlayerDataToFile();
        else
            Debug.LogError("PlayerDM does not exist!");
        }

    // Player ID
    public int GetPlayerID() { return playerID; }
    public void SetPlayerID(int playerID) { this.playerID = playerID; Save(); }

    // Player username
    public string GetUsername() { return username; }
    public void SetUsername(string userName) { this.username = userName; Save(); }

    // Player PIN
    public int GetPin() { return pin; }
    public void SetPIN(int pin) { this.pin = pin; Save(); }

    // Player currency
    public int GetCurrency() { return currency; }
    public void SetCurrency(int currency) // Do not call. Call AddCurrency
    { this.currency = currency; Save(); }
    public void AddCurrency(int currency) { SetCurrency(GetCurrency() + currency); } // works for negative values as well.

    // Scores
    public int GetScore() { return score; }
    public void SetScore(int score) // Do not call. Call AddScore
    { this.score = score; Save(); }
    public void AddScore(int score) { SetScore(GetScore() + score); }

    // Avatar cosmetics
    public int[] GetAvatar() { return new int[] { avatarIndex, hatIndex, shirtIndex }; }
    public void SetAvatar(int avatarIndex, int hatIndex, int shirtIndex)
    {
        this.avatarIndex = avatarIndex;
        this.hatIndex = hatIndex;
        this.shirtIndex = shirtIndex;
        Save();
    }

    // Contact information
    public List<ContactInfo> GetContacts() { return new List<ContactInfo>(contacts); }
    public void AddContact(string name, string phoneNumber)
    { contacts.Add(new ContactInfo(name, phoneNumber)); Save(); }
    public void RemoveContact(string phoneNumber)
    { contacts.RemoveAll(c => c.phoneNumber == phoneNumber); Save(); } // Delete a contact based on the number

    // user settings
    public void ChangeEffectsVolume(float volume) { effectsVolume = (int)volume; }
    public void ChangeMusicVolume(float volume) { musicVolume = (int)volume; }

    public int GetEffectsVolume() { return effectsVolume; }
    public int GetMusicVolume() { return musicVolume; }

    private void OnApplicationQuit()
    {
        // REMOVE WHEN FINALIZED

        // ensures no duplicate IDs are saved
        unlockedAvatarIDs.Clear();
        unlockedClothesIDs.Clear();
        unlockedHatsIDs.Clear();
        playerDM.SavePlayerDataToFile();
    }

}

// Ensure names and phone numbers stay paired up
[System.Serializable]
public class ContactInfo
{
    public string name;
    public string phoneNumber;

    public ContactInfo(string name, string phoneNumber)
    {
        this.name = name;
        this.phoneNumber = phoneNumber;
    }
}
