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

    // Keeps track of each games question pool count
    public int currentPoolCount;
    // Keeps track of current question index - TEMPORARY SOLUTION
    public int currentQuestionIndex;

    public int randomIndex;

    private void Start()
    {
        currentPoolCount= 0;
        currentQuestionIndex= 0;
        randomIndex = 0;
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

    // generates a random number and selects a random question from the pool
    public void NextQuestion() 
    {
        // Choose a random question from the pool of questions
        do
        {
            randomIndex = UnityEngine.Random.Range(0, currentPoolCount);
        } while (randomIndex == currentQuestionIndex);

        nextQuestion?.Invoke(randomIndex);
        // update currentQuestionIndex
        currentQuestionIndex = randomIndex;
    }

    // Score disabled and state cleared at end
    public void EndGame() { gameFinished?.Invoke(); disableScore(); clearState(); }

    public void enableScore() { score.gameObject.SetActive(true); }

    public void disableScore() { score.gameObject.SetActive(false); }

}
