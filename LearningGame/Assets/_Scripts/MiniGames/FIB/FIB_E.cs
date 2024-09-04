using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FIB_E : MonoBehaviour
{
    Card nextCard;
    [SerializeField] private GameObject thisPage; // reference to self (screen) to disable at end of game

    // ref to UI elements within FIB
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TMP_InputField userInput;
    // figure out what to do with settings buttons later. maybe make a prefab of them and do a singleton

    private int currentScore; //should find a way to integrate this with the score system

    private void OnEnable()
    {
        currentScore = 0;
        ExperimentalGM.instance.SetGameMode(ExperimentalGM.GameMode.FIB);
        ExperimentalGM.instance.StartGame(); // event call
        RequestCard(); // request new card at start of game
    }

    private void Update()
    {
        if (currentScore == 40) {
            EndGame();
        }
    }

    //ENDGAME()
    private void EndGame()
    {
        ExperimentalGM.instance.EndGame(); //event call
        ExperimentalGM.instance.GoHome(thisPage); // calls GM to close this page
    }

    // - request from GM database
    public void RequestCard()
    {
        nextCard = ExperimentalGM.instance.GetRandomCard(); // should rename GM's function
        SetUIData();
    }

    // Sets UI elements to card question/answer
    private void SetUIData()
    {
        questionText.text = nextCard.question;
    }

    // Checks if user input satisfies the answer
    public void CheckAnswer()
    {
        if (userInput.text == "")
        {
            Debug.Log("Please enter text");
            return;
        } else if (userInput.text == nextCard.answer)
        {
            // increment score
            ExperimentalGM.instance.IncrementPoints();
            currentScore += 10;
        }
        else {
            Debug.Log("answer is false");
        }

        // clear user input
        userInput.text = "";
        RequestCard();

    }
}
