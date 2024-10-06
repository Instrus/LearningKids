using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// The current game manager, better than the old!
public class ExperimentalGM : MonoBehaviour
{
    public GameMode currentMode;
    [SerializeField] public CardDatabase cardDB; // database for all available cards
    [SerializeField] public GameObject scoreSystem; // at start of game: enable score system

    // singleton pattern
    public static ExperimentalGM instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

       currentMode = GameMode.None;
    }

    private void Start()
    {
        // lock framerate to 60 fps
        Application.targetFrameRate = 60;
    }

    // GameManager will have to handle all games depending on Game mode.
    public enum GameMode { None, FlashCards, FIB, Matching }

    // Events - Event call functions (event?.Invoke() = event call)
    public event Action gameStarted, gameFinished, scoreIncremented;

    public void SetGameMode(GameMode mode) { currentMode = mode; }
    public void StartGame() { EnableScore(); gameStarted?.Invoke(); } // Score enabled at start of any minigame
    public void EndGame() { gameFinished?.Invoke(); DisableScore(); ClearState(); } // Score disabled and state cleared at end
    public void IncrementPoints() { scoreIncremented?.Invoke(); } // calls event when user answers a question correctly

    public void EnableScore() { scoreSystem.gameObject.SetActive(true); }
    public void DisableScore() { scoreSystem.gameObject.SetActive(false); }
    public void ClearState() { currentMode = GameMode.None; }

    // initialize the Random class for generating numbers
    System.Random Random = new System.Random();
    public Card GetRandomCard()
    {
        // For Flashcards
        if (currentMode == ExperimentalGM.GameMode.FlashCards)
        {
            int randomNum = Random.Next(0, cardDB.FlashCards.Length);
            return cardDB.FlashCards[randomNum];
        }

        // For FIB
        if (currentMode == ExperimentalGM.GameMode.FIB)
        {
            int randomNum = Random.Next(0, cardDB.FIBCards.Length);
            return cardDB.FIBCards[randomNum];
        }

        return null;
    }

    public GameObject homeScreen; // ref to home screen to enable it
    public void GoHome(GameObject screen) // called at end of game to go home
    { screen.SetActive(false); homeScreen.SetActive(true); } // has to be called externally because of argument

}
