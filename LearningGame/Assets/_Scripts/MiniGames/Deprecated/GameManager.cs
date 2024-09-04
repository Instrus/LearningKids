using UnityEngine;
using System;

// GameManager oversees all events during runtime of the application
public class GameManager : MonoBehaviour
{

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

    // tracks current running game's question pool count
    public int poolCount;
    // track previous question index - prevents two of the same question in a row
    public int previousQuestionIndex;
    // used for generating a random number for picking a random question from pool
    public int randomIndex;
    // tracks users score during minigame (gameobject found within Unity Hierarchy)
    public GameObject score;

    // game states (game modes / minigames)
    public enum GameMode
    {
        None,
        FlashCards,
        FIB,
        Matching
    }

    // tracks current game state / mode
    public GameMode gameState;

    // events
    public event Action gameStarted, gameFinished, scoreIncremented;
    public event Action<int> nextQuestion;

    private void Start()
    {
        poolCount= 0;
        previousQuestionIndex= 0;
        randomIndex = 0;
    }

    // changes gameState
    public void changeState(string newState)
    {
        switch(newState)
        {
            case "FlashCards":
                gameState = GameMode.FlashCards;
                break;
            case "FIB":
                gameState = GameMode.FIB;
                break;
            case "Matching":
                gameState = GameMode.Matching;
                break;
            default: 
                gameState = GameMode.None;
              break;
        }
    }

    public void clearState() { gameState = GameMode.None; }

    // Event call functions (event?.Invoke() = event call)

    // Score enabled at start of any minigame
    public void StartGame() { gameStarted?.Invoke(); enableScore(); }

    // calls event when user answers a question correctly
    public void IncrementPoints() { scoreIncremented?.Invoke(); }

    // selects a random question from the pool of questions
    public void NextQuestion() 
    {
        // select new random question - ensures no two same questions in a row
        do
        {
            randomIndex = UnityEngine.Random.Range(0, poolCount);
        } while (randomIndex == previousQuestionIndex);

        // event call
        nextQuestion?.Invoke(randomIndex);
        // update index
        previousQuestionIndex = randomIndex;
    }

    // Score disabled and state cleared at end
    public void EndGame() { gameFinished?.Invoke(); disableScore(); clearState(); }

    public void enableScore() { score.gameObject.SetActive(true); }

    public void disableScore() { score.gameObject.SetActive(false); }

}
