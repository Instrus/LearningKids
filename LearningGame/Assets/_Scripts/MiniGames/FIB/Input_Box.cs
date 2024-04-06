using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// FIB sub_class
public class Input_Box : MonoBehaviour
{
    // reference to self
    public TMP_InputField input;
    public FIB_Manager points;

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
            // Get another question
            questionBox.RandomQuestion();
        }
        
    }

    // Clear questionsAnswered and input box when game over
    void clearData()
    {
        if (GameManager.instance.gameState == GameManager.GameMode.FIB)
        {
            Debug.Log("FIB cleared.");
            answeredQuestions = 0;
            input.text = string.Empty;
        }
    }

}
