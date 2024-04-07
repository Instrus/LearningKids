using TMPro;
using UnityEngine;

// FIB's Input box - determines most of the log of FIB. Works with GameManager like every other.
public class Input_Box : MonoBehaviour
{
    // reference to self
    public TMP_InputField input;

    // reference to question box (to get answer)
    [SerializeField] public Question_Box questionBox;

    // keeps track of how many questions the user answered
    public int answeredQuestions;

    private void Start()
    {
        GameManager.instance.gameFinished += clearData;
    }

    // Update used to check how many answers were answered. If cap met, end game.
    private void Update()
    {
        if (answeredQuestions == 5) {
            Debug.Log("Game over");
            GameManager.instance.EndGame();
        }
    }

    public void checkAnswer()
    {

        // if user answered 10th question (or other), end the game.
        if (answeredQuestions < 5)
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
            // get another question
            questionBox.RandomQuestion();
        }
        
    }

    // clear questionsAnswered and input box when game over
    void clearData()
    {
        // function only runs when the right game mode is selected
        if (GameManager.instance.gameState == GameManager.GameMode.FIB)
        {
            Debug.Log("FIB cleared.");
            answeredQuestions = 0;
            input.text = string.Empty;
        }
    }

}
