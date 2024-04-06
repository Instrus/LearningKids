using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;
using TMPro;

public class ScoreManager : MonoBehaviour
{


//creating arrays to hold 9 randomly generated leaderboard info and 1 for the actual player
 private string[] ListOfUsers = new string[9] {"jocularsummerson","starkbardell","gracefulquale","flagrantscrooge","trashytoppit", "escentboythorn" , " adol","oafishborum", "passivegrinder"};
 private int[] ListOfScores = new int[9];



//this is way i found to do it, it is very redudant and probabaly an easier way to do this
 [SerializeField] TextMeshProUGUI score_text0, score_text1, score_text2, score_text3, score_text4, score_text5, score_text6, score_text7, score_text8, score_text9;
[SerializeField] TextMeshProUGUI user_text0 , user_text1, user_text2, user_text3, user_text4, user_text5, user_text6, user_text7, user_text8, user_text9;

    // Functions: (F1): Fetch score from PlayerData, (F2): Send score to scoresPage (will need a function for scoresPage to call)
    // (F3): set player name to middle of leaderboard (F4): generate the random scores of other players
    void Start()
    {
        // subscribe to events. (all games)
        fetchScore();
        // 
    }

    public void fetchScore()
    {
        // store score, call postScore
        int score = PlayerData.instance.getScore();
        Debug.Log(score);
        postScore(score);
    }
    // Updates 
    public void postScore(int playerScore) //designed for text mesh pro.
    {
        //intialize arrays for names and scores
        GenerateNames();
        GenerateScores(playerScore);
        //pass values to fields, i will comment this out until you have your fields set
        /*
        user_text0.text = ListOfUsers[0];
        user_text1.text = ListOfUsers[1];
        user_text2.text = ListOfUsers[2];
        user_text3.text = ListOfUsers[3];
        user_text4.text = ListOfUsers[4];
        user_text5.text = ListOfUsers[5];
        user_text6.text = ListOfUsers[6];
        user_text7.text = ListOfUsers[7];
        user_text8.text = ListOfUsers[8];
        user_text9.text = ListOfUsers[9];
         
        score_text0.text = ListOfScores[0].ToString();
        score_text1.text = ListOfScores[1].ToString();
        score_text2.text = ListOfScores[2].ToString();
        score_text3.text = ListOfScores[3].ToString();
        score_text4.text = ListOfScores[4].ToString();
        score_text5.text = ListOfScores[5].ToString();
        score_text6.text = ListOfScores[6].ToString();
        score_text7.text = ListOfScores[7].ToString();
        score_text8.text = ListOfScores[8].ToString();
        score_text9.text = ListOfScores[9].ToString();
        */
    }

    public void GenerateNames()
    {
        ListOfUsers[4] = PlayerData.instance.getUsername();
    }
    public void GenerateScores(int playerScore)
    {
        ListOfScores[4] = playerScore;//player score (5th place) - middle ranking

        //generate 4 scores higher than the player
        for(int i = 0; i < 4; i++){
            ListOfScores[i] = Random.Range(playerScore, playerScore + 35);
        }

        //generate 5 scores below player score -- this could potentially lead to negative numbers, but if the player gets 4 right in the showcase it wont (10pts per question)
        for(int i = 5; i < 9; i++){
            ListOfScores[i] = Random.Range(playerScore, playerScore - 35);
        }
        //then sort array in numerical order
        Array.Sort(ListOfScores);
        foreach (int i in ListOfScores)
        {
         Console.WriteLine(i);
        }
        
    }
}
