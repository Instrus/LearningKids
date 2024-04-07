using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class FlashCardsAnswerSet : MonoBehaviour
{

    // Store this script in AnswerSet to keep track of values (such as answered questions)

    // reference to each answer box button
    [SerializeField] GameObject[] answerBoxes;
    // reference to each asnwer box texts
    [SerializeField] TextMeshProUGUI[] text;

    // for compaing answer
    [SerializeField] Question_Box questionBox;

    // keeps track of how many questions the user answers.
    public int answeredQuestions;


    private void Start()
    {
        GameManager.instance.gameFinished += clearData;
        // test values for now
        text[0].text = "Jesse";
        text[1].text = "Walter";
        text[2].text = "Knocks";
        text[3].text = "Danger";
    }

    // update used to check how many answers were answered. If cap met, end game.
    private void Update()
    {
        if (answeredQuestions == 5)
        {
            Debug.Log("Game over");
            GameManager.instance.EndGame();
        }
    }

    // clear questionsAnswered and input box when game over
    void clearData()
    {
        // function only runs when the right game mode is selected
        if (GameManager.instance.gameState == GameManager.GameMode.FlashCards)
        {
            Debug.Log("FlashCards cleared.");
            answeredQuestions = 0;
        }
    }

    // index = which button was selected. get the text of that button and compare it to answer
    public void checkAnswer(int index)
    {
        string userInput = text[index].text;

        // get answer from question box
        string answer = questionBox.answers[questionBox.questionIndex];

        // If the user answer the question correctly:
        if (userInput == answer)
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
