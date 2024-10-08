using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using Unity.VisualScripting;
using Firebase.Extensions;
using UnityEngine.Rendering;

public class DB_Connections : MonoBehaviour
{



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

    public GameObject usernamePanel, userProfilePanel, leaderboardPanel, leaderboardContent, userDataPrefab;

    public TMP_Text profileUsernameTxt, profileUserScoreTxt, errorUsernameTxt;

    public TMP_InputField usernameInput;
    public int score,totalUsers=0;
    public string username ="";
    private DatabaseReference db;


    void Start()
    {
        //Initialize database first
        FirebaseIntialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 public void SignInWithUserName(){
    StartCoroutine(CheckUserExistInDatabase());
 }
public void CloseLeaderBoard(){
        if(leaderboardContent.transform.childCount>0){
            for(int i=0;i<leaderboardContent.transform.childCount; i++){
                Destroy(leaderboardContent.transform.GetChild(i).gameObject);
            }
        }
        leaderboardPanel.SetActive(false);
        userProfilePanel.SetActive(true);
 }

 public void SignOut(){
    PlayerPrefs.DeleteKey("PlayerID");
    PlayerPrefs.DeleteKey("Username");
    usernameInput.text="";
    profileUsernameTxt.text="";
    profileUserScoreTxt.text="";
    errorUsernameTxt.text="";
    score=0;
    username="";
    usernamePanel.SetActive(true);
    userProfilePanel.SetActive(false);

 }


    void FirebaseIntialize(){
        db = FirebaseDatabase.DefaultInstance.GetReference("/Leaderboard");

        db.ChildAdded+=HandleChildAdded;
      
      //now fetch total users
      GetTotalUsers();

      //check if player already login then show user profile, otherwise show username sign in page
      StartCoroutine(FetchUserProfileData(PlayerPrefs.GetInt("PlayerID")));
    
    }

public void ShowLeadboard(){
    StartCoroutine(FetchLeaderBoardData());
}
    //need to create firebase child added function which check if new user score added or not
    void HandleChildAdded(object sender, ChildChangedEventArgs args){
        if(args.DatabaseError!=null){
            return;
        }
        //if new child added then we need to fetch total users numbers in db
        GetTotalUsers();
        
    }

    void GetTotalUsers(){
        //Get total users from firebase
        db.ValueChanged+=(object sender2,ValueChangedEventArgs e2)=>
        {
            if(e2.DatabaseError!=null){
                Debug.Log(e2.DatabaseError.Message);
                return;
            }

            totalUsers=int.Parse(e2.Snapshot.ChildrenCount.ToString());
            Debug.Log("total users in database:"+totalUsers);

        };
    }

    IEnumerator CheckUserExistInDatabase(){

            var task = db.OrderByChild("username").EqualTo(usernameInput.text).GetValueAsync();
            yield return new WaitUntil(()=>task.IsCompleted);

            if(task.IsFaulted){
                Debug.LogError("Invalid Error");
                errorUsernameTxt.text="Invalid Error";
            }

            else if(task.IsCompleted){
                DataSnapshot snapshot=task.Result;
                if(snapshot!=null && snapshot.HasChildren){
                    Debug.Log("Username Exist");
                    errorUsernameTxt.text="Username Already Exist";
                }
                else{
                    Debug.Log("Username Not Exist");

                    //push new user data
                    //set player pref USER ID and USERname for login purpose
                    //show profil
                    PushUserData();
                    PlayerPrefs.SetInt("PlayerID",totalUsers+1);
                    PlayerPrefs.SetString("username",usernameInput.text);
                    //PlayerPrefs.SetInt("score",0);
                    StartCoroutine(delayFetchProfile());
                }
                
            }

IEnumerator delayFetchProfile(){
    yield return new WaitForSeconds(1f);
    StartCoroutine(FetchUserProfileData(totalUsers));
}

void PushUserData(){
    db.Child("User_"+(totalUsers+1).ToString()).Child("username").SetValueAsync(usernameInput.text);
    db.Child("User_"+(totalUsers+1).ToString()).Child("score").SetValueAsync(0);
    
}


    }
    IEnumerator FetchUserProfileData(int playerID){
        if(playerID!=0){
        var task = db.Child("User_"+playerID.ToString()).GetValueAsync();
            yield return new WaitUntil(()=>task.IsCompleted);

            if(task.IsFaulted){
                Debug.LogError("Invalid Error");
            }

            else if(task.IsCompleted){
                DataSnapshot snapshot=task.Result;
                if(snapshot!=null && snapshot.HasChildren){
                    //here we fetch all user data from db and puut in variables and text
                    username=snapshot.Child("username").Value.ToString();
                    score=int.Parse(snapshot.Child("score").Value.ToString());
                    profileUsernameTxt.text=username;
                    profileUserScoreTxt.text=""+score;
                    userProfilePanel.SetActive(true);
                    usernamePanel.SetActive(false);
                }
        }
        
    }
    }
    IEnumerator FetchLeaderBoardData(){

        var task = db.OrderByChild("score").LimitToLast(10).GetValueAsync();
            yield return new WaitUntil(()=>task.IsCompleted);

            if(task.IsFaulted){
                Debug.LogError("Invalid Error");
            }

            else if(task.IsCompleted){
                DataSnapshot snapshot=task.Result;
                Debug.Log("ShowLeaderboard");
                List<LeaderboardData> listLeaderboardEntry=new List<LeaderboardData>();
                foreach(DataSnapshot childSnapShot in snapshot.Children){
                    string username2= childSnapShot.Child("username").Value.ToString();
                    int score = int.Parse(childSnapShot.Child("score").Value.ToString());

                    listLeaderboardEntry.Add( new LeaderboardData(username2,score));
                }
                DisplayLeaderboard(listLeaderboardEntry);
            
            }



        
    }


    void DisplayLeaderboard(List<LeaderboardData> leaderboardData){

        int rankCount=0;
        for(int i=leaderboardData.Count-1; i>=0; i--)
       {
            rankCount=rankCount+1;
            //spawn user leaderboard data ui
            GameObject obj= Instantiate(userDataPrefab);
            obj.transform.parent=leaderboardContent.transform;
            obj.transform.localScale=Vector3.one;

            //obj.GetComponent<UserDataUI>().userRankTxt.text="Rank"+rankCount;
            obj.GetComponent<UserDataUI>().usernameTxt.text=""+leaderboardData[i].username;
            obj.GetComponent<UserDataUI>().userScoreTxt.text=""+leaderboardData[i].score;
            
       }
        leaderboardPanel.SetActive(true);
        userProfilePanel.SetActive(false);
    }
}

public class LeaderboardData{
    public string username;
    public int score;
    public LeaderboardData(string username, int score){
        this.username = username;
        this.score = score;
    }
}