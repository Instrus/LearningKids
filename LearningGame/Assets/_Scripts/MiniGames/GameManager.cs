using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    // For each game, call any events needed at the time. Within each function called a check on state will occur.

    // singleton pattern
    public static GameManager instance { get; private set; }
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

    // events
    public event Action gameStarted, gameFinished, scoreIncremented;
    public event Action<int> nextQuestion;

    // game states (game modes / minigames)
    public enum GameMode
    {
        None,
        FlashCards,
        FIB,
        Matching
    }

    public GameMode gameState;

    public GameObject score;


    // changes gameState
    public void changeState(string newState)
    {
        switch(newState)
        {
            case "FlashCards":
                gameState = GameMode.FlashCards;
                Debug.Log("Flashcards");
                break;
            case "FIB":
                gameState = GameMode.FIB;
                Debug.Log("FIB");
                break;
            case "Matching":
                gameState = GameMode.Matching;
                Debug.Log("Matching");
                break;
            default: 
                gameState = GameMode.None;
                Debug.Log("Default");
                break;
        }
    }

    public void clearState() { gameState = GameMode.None; }

    // Event calls

    // Score enabled at start
    public void StartGame() { gameStarted?.Invoke(); enableScore(); }

    public void IncrementPoints() { scoreIncremented?.Invoke(); }

    // Probably need to generate a random number, how do I pass the random number to both functions? - EXPERIMENTAL
    public void NextQuestion() { 
        
        int randomIndex = UnityEngine.Random.Range(0, 5);
        nextQuestion?.Invoke(randomIndex); }

    // Score disabled and state cleared at end
    public void EndGame() { gameFinished?.Invoke(); disableScore(); clearState(); }

    public void enableScore() { score.gameObject.SetActive(true); }

    public void disableScore() { score.gameObject.SetActive(false); }

}
