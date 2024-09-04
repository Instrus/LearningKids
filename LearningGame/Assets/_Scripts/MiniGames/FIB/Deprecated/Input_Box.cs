using TMPro;
using UnityEngine;

// acts as FIBs minigame manager
public class Input_Box : MonoBehaviour
{
    // reference to self (for evaluating user input)
    public TMP_InputField input;
    // reference to question box (to get answer)
    [SerializeField] public Question_Box questionBox;

    // keeps track of how many questions the user answered
    public int answeredQuestions;

    private void OnEnable()
    {
        GameManager.instance.gameFinished += clearData;
    }

    private void OnDisable()
    {
        GameManager.instance.gameFinished -= clearData;
    }

    // Update used to check how many answers were answered. If cap met, end game.
    private void Update()
    {
        if (answeredQuestions == 5)
        {
            Debug.Log("Game over");
            GameManager.instance.EndGame();
        }
    }

    public void checkAnswer()
    {
        // get user input from field input text
        string inputText = input.text;
        // get answer from question box
        string answer = questionBox.answers[questionBox.questionIndex];

        // If the user answer the question correctly:
        if (inputText == answer)
        {
            Debug.Log("Correct!");
            GameManager.instance.IncrementPoints();
        }
        else
        {
            Debug.Log("False!");
        }

        // increment count of answered questions
        answeredQuestions += 1;
        GameManager.instance.NextQuestion();


        // clear answer
        input.text = "";
    }

    // clear questionsAnswered and input box when game over
    void clearData()
    {
        // function only runs when the right game mode is selected
        if (GameManager.instance.gameState == GameManager.GameMode.FIB)
        {
            answeredQuestions = 0;
            input.text = string.Empty;
        }
    }

}
