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
using Firebase;

/*
    this code will
    1. Initialize firebase DB
    2. Get total users
    3. Check user exist in DB
    4. Fetch User Profile Data
    5. Fetch Leader Board Data
    6. Display Leaderboard UI
    7. Add UI events sign in, sign out, Close leaderboard
    8. Make sure in Assets folder you mus th ave streaming asset foler if not you have to close and open
    project again. If streaming folder is still not there create new one and name it StreamingAssetsFolder and put the google-services.json file there.
    */

public class DB_Connections : MonoBehaviour
{

    private DatabaseReference db;

    public GameObject usernamePanel, userProfilePanel, leaderboardPanel, leaderboardContent, userDataPrefab;
    public TMP_Text profileUsernameTxt, profileUserScoreTxt, errorText;
    
    // inputs
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    // pages (navigation)
    public GameObject login_Page;
    public GameObject PIN_Screen;

    public string username = "";
    public int score;
    public string password;

    public PlayerData playerData;

    public int totalUsers = 0;

    void Start() 
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Firebase.DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Initialize the Firebase Database reference
                db = FirebaseDatabase.DefaultInstance.GetReference("/Leaderboard");
                Debug.Log("Firebase has been properly initialized.");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies:" + dependencyStatus);
            }
        });
    }

    public void Login() { 
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.LogWarning("Please enter a username and password.");
            errorText.text = "Please enter a username and password.";
            return;
        }
        StartFindUser(); 
    }

    public void StartFindUser()
    {
        StartCoroutine(FindUserCoroutine());
    }

    private IEnumerator FindUserCoroutine()
    {
        int playerID = 1; // start at 1 for the first user
        bool userFound = false;

        //var task = db.OrderByChild("username").EqualTo(usernameInput.text).GetValueAsync();
        var task = db.GetValueAsync();

        // wait for the task to complete
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Task Failed - FindUser: " + task.Exception);
            yield break; // exit coroutine if there's an error
        }

        if (task.IsCompletedSuccessfully)
        {
            print("task success");
            DataSnapshot snapshot = task.Result;

            if (snapshot != null && snapshot.HasChildren) // snapshot is randomly null or snapshot.HasChildren randomly fails
            {
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    string username = childSnapshot.Child("username").Value.ToString();
                    Debug.Log("Checking " + username);

                    if (username == usernameInput.text)
                    {
                        int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                        Debug.Log("User found: " + username + ", Score: " + score);
                        playerData.SetPlayerID(int.Parse(childSnapshot.Key));
                        userFound = true;
                        break;
                    }
                    // increment playerID for each record
                    playerID++;
                }

                if (userFound)
                {
                    playerData.SetUsername(usernameInput.text);
                    StartCoroutine(CheckPasswordCoroutine());
                }
                else
                {
                    Debug.Log("Username does not exist.");
                    errorText.text = "Username does not exist!";
                    playerData.SetPlayerID(-1);
                    playerData.SetScore(0);
                    playerData.SetUsername("");
                }
            }
            else
            {
                // snapshot error
                errorText.text = "Error: failed to fetch username.";
                Debug.Log("Error: failed to fetch username.");
                playerData.SetPlayerID(-1);
                playerData.SetScore(0);
                playerData.SetUsername("");

                if (snapshot == null)
                {
                    Debug.LogWarning("Snapshot is null");
                } else if (!snapshot.HasChildren)
                {
                    Debug.LogWarning("Snapshot has no children");
                }
                
            }
        }
    }


    private IEnumerator CheckPasswordCoroutine()
    {
        bool passwordCheck = false;

        // fetch the data using usernameInput.text
        var task = db.OrderByChild("username").EqualTo(usernameInput.text).GetValueAsync();

        // wait until the task completes
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Task Failed - CheckPassword: " + task.Exception);
            yield break; // exit coroutine if there's an error
        }

        if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot != null && snapshot.HasChildren)
            {
                var enumerator = snapshot.Children.GetEnumerator();
                if (enumerator.MoveNext()) // Move to the first element
                {
                    DataSnapshot firstChild = enumerator.Current;

                    if (firstChild != null)
                    {
                        Debug.Log("Checking password");
                        string password = firstChild.Child("password").Value.ToString();

                        // check if the password matches
                        if (password == passwordInput.text)
                        {
                            Debug.Log("Password entered correctly: " + password);
                            passwordCheck = true;
                        }
                    }
                }

                if (passwordCheck)
                {
                    // if password is correct, fetch user data
                    StartCoroutine(FetchUserProfileData(playerData.GetPlayerID()));
                    // Go to PIN screen if user and pass were found
                    // GoPIN(); 
                }
                else
                {
                    Debug.Log("Account not found.");
                    errorText.text = "Account not found.";
                    playerData.SetPlayerID(-1);
                    playerData.SetScore(0);
                    playerData.SetUsername("");
                }
            }
            else
            {
                // snapshot error
                errorText.text = "Error: failed to fetch password";
                Debug.LogWarning("Error: failed to fetch password.");
                playerData.SetPlayerID(-1);
                playerData.SetScore(0);
                playerData.SetUsername("");

                if (snapshot == null)
                {
                    Debug.LogWarning("Snapshot is null");
                }
                else if (!snapshot.HasChildren)
                {
                    Debug.LogWarning("Snapshot has no children");
                }
            }
        }
    }


    public void CreateAccount()
    {
        PushUserData();
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            errorText.text = "Please enter a username and password";
            return;
        }
            
        usernameInput.text = "";
        passwordInput.text = "";
        errorText.text = "Account created!";
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
        errorText.text = "";
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

                // reset login page UI components
                passwordInput.text = "";
                usernameInput.text = "";
            }
        }
    }

    public void ShowLeadboard() { StartCoroutine(FetchLeaderBoardData()); }

    IEnumerator FetchLeaderBoardData()
    {

        var task = db.OrderByChild("score").LimitToLast(10).GetValueAsync();
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
            await db.Child(newUserKey).Child("password").SetValueAsync(passwordInput.text);
        }
        else
        {
            // Handle the case where no data exists in the database
            Debug.LogWarning("No data exists in the database.");

            // Optionally, start with the first user if no data exists
            await db.Child("1").Child("username").SetValueAsync(usernameInput.text);
            await db.Child("1").Child("score").SetValueAsync(0); //default 0
            await db.Child("1").Child("password").SetValueAsync(passwordInput.text);

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