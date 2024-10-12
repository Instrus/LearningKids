using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using Unity.VisualScripting;
using Firebase.Extensions;
using UnityEngine.Rendering;
using System.Linq;
using System;

/*
    this code will
    1. Initialize firebase DB
    2. Get total users
    3. Check user exist in DB
    4. Fetch User Profile Data
    5. Fetch Leader Board Data
    6. Display Leaderboard UI
    7. Add UI events SIgn in, Signout, Close leaderboard
    8. Make sure in Assets folder you mus th ave streaming asset foler if not you have to close and open
    project again. If streaming folder is still not there create new one and name it StreamingAssetsFolder and put the google-services.json file there.
    */

public class DB_Connections : MonoBehaviour
{
    // find a way to use playerID in PlayerData (set on login, clear on logout)

    private DatabaseReference db;

    public GameObject usernamePanel, userProfilePanel, leaderboardPanel, leaderboardContent, userDataPrefab;
    public TMP_Text profileUsernameTxt, profileUserScoreTxt, errorUsernameTxt;
    public TMP_InputField usernameInput;

    public GameObject login_Page;
    public GameObject PIN_Screen;

    public string username = "";
    public int score;
    public PlayerData playerData;

    public int totalUsers = 0;

    void Start() 
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        FirebaseIntialize(); 
    }
    
    void FirebaseIntialize() { db = FirebaseDatabase.DefaultInstance.GetReference("/Leaderboard"); }
    
    public void Login() { FindUser(); }

    public async void FindUser()
    {
        int playerID = 1; // Start at 1 for the first user
        bool userFound = false;

        try
        {
            var task = db.OrderByChild("username").EqualTo(usernameInput.text).GetValueAsync();
            DataSnapshot snapshot = await task;

            if (snapshot != null && snapshot.HasChildren)
            {
                

                // Loop through each result in the snapshot
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    string username = childSnapshot.Child("username").Value.ToString();

                    // Check if the username matches
                    if (username == usernameInput.text)
                    {
                        int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                        Debug.Log("User found: " + username + ", Score: " + score);
                        // Set the player ID (index of the user in the leaderboard)
                        playerData.SetPlayerID(int.Parse(childSnapshot.Key));
                        userFound = true;
                        break; // exit loop if the user is found
                    }

                    playerID++; // Increment playerID for each record
                }

                if (!userFound)
                {
                    Debug.Log("Username does not exist.");
                    errorUsernameTxt.text = "Username does not exist!";
                    playerData.SetPlayerID(-1);
                }
            }
            else
            {
                // No matching users found
                errorUsernameTxt.text = "Username does not exist!";
                playerData.SetPlayerID(-1);
                Debug.Log("Username does not exist.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Task Failed - FindUser: " + ex.Message);
        }

        // go to pin if user was found
        //if (userFound)
        //{
        //    GoPIN();
        //}

        //print("playerID: " + playerData.GetPlayerID());
        StartCoroutine(FetchUserProfileData(playerData.GetPlayerID()));
    }

    public void CreateAccount()
    {
        PushUserData();
    }

    public void GoPIN()
    {
        if (PIN_Screen!= null && login_Page != null) {
            PIN_Screen.SetActive(true);
            login_Page.SetActive(false);
        }
    }

    public void SignOut()
    {
        // clear PlayerData ID
        usernameInput.text = "";
        // clear UI components
        profileUsernameTxt.text = "";
        profileUserScoreTxt.text = "";
        errorUsernameTxt.text = "";
        // player data
        username = "";
        score = 0;
        // panels
        usernamePanel.SetActive(true);
        userProfilePanel.SetActive(false);
    }

    // unused
    IEnumerator FetchUserProfileData(int playerID)
    {
        if (playerID != 0)
        {
            var task = db.Child(playerID.ToString()).GetValueAsync();
            yield return new WaitUntil(() => task.IsCompleted);

            if (task.IsFaulted)
                print("Task failed - FetchUserProfileData");

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.HasChildren)
                {
                    //here we fetch all user data from db and puut in variables and text
                    username = snapshot.Child("username").Value.ToString();
                    score = int.Parse(snapshot.Child("score").Value.ToString());
                    profileUsernameTxt.text = username;
                    profileUserScoreTxt.text = "" + score;
                    userProfilePanel.SetActive(true);
                    usernamePanel.SetActive(false);
                }
            }
        }
    }

    public void ShowLeadboard() { StartCoroutine(FetchLeaderBoardData()); }

    IEnumerator FetchLeaderBoardData()
    {

        var task = db.OrderByChild("score").LimitToLast(3).GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            print("Invalid error");
        }

        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            print("Show leaderboard");
            List<LeaderboardData> listLeaderboardEntry = new List<LeaderboardData>();
            foreach (DataSnapshot childSnapShot in snapshot.Children)
            {
                string username2 = childSnapShot.Child("username").Value.ToString();
                int score = int.Parse(childSnapShot.Child("score").Value.ToString());

                listLeaderboardEntry.Add(new LeaderboardData(username2, score));
            }
            DisplayLeaderboard(listLeaderboardEntry);
        }
    }

    void DisplayLeaderboard(List<LeaderboardData> leaderboardData)
    {

        int rankCount = 0;
        for (int i = leaderboardData.Count - 1; i >= 0; i--)
        {
            rankCount = rankCount + 1;
            //spawn user leaderboard data ui
            GameObject obj = Instantiate(userDataPrefab);
            // Use SetParent for UI elements instead of setting the parent directly
            obj.transform.SetParent(leaderboardContent.transform, false);  // false keeps the local scale and position intact
            obj.transform.localScale = Vector3.one;

            //obj.GetComponent<UserDataUI>().userRankTxt.text="Rank"+rankCount;
            obj.GetComponent<UserDataUI>().usernameTxt.text = "" + leaderboardData[i].username;
            obj.GetComponent<UserDataUI>().userScoreTxt.text = "" + leaderboardData[i].score;

        }
        leaderboardPanel.SetActive(true);
        userProfilePanel.SetActive(false);
    }

    public void CloseLeaderBoard()
    {
        if (leaderboardContent.transform.childCount > 0)
        {
            for (int i = 0; i < leaderboardContent.transform.childCount; i++)
            {
                Destroy(leaderboardContent.transform.GetChild(i).gameObject);
            }
        }
        leaderboardPanel.SetActive(false);
        userProfilePanel.SetActive(true);
    }

    // call this to update values or create a new user
    public async void PushUserData()
    {
        if (usernameInput.text == "")
            return;

        DataSnapshot snapshot = await db.GetValueAsync();

        if (snapshot.Exists && snapshot.HasChildren)
        {
            int recordCount = (int)snapshot.ChildrenCount;

            string newUserKey = (recordCount + 1).ToString();
            // Push username and score
            await db.Child(newUserKey).Child("username").SetValueAsync(usernameInput.text);
            await db.Child(newUserKey).Child("score").SetValueAsync(0); // might need to get playerData score? to update
        }
        else
        {
            // Handle the case where no data exists in the database
            Debug.LogWarning("No data exists in the database.");

            // Optionally, start with the first user if no data exists
            await db.Child("1").Child("username").SetValueAsync(usernameInput.text);
            await db.Child("1").Child("score").SetValueAsync(0); //default 0

            Debug.Log("First user added: " + usernameInput.text);
        }
    }


}

public class LeaderboardData
{
    public string username;
    public int score;
    public LeaderboardData(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}