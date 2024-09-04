using TMPro;
using UnityEngine;

// acts as the FlashCards minigame manager
public class FlashCardsAnswerSet : MonoBehaviour
{
    // reference to each answer button texts
    [SerializeField] TextMeshProUGUI[] answerTexts;
    // reference to question_box to compare answers between a buttons text and questionbox's answer
    [SerializeField] Question_Box questionBox;

    // keeps track of how many questions the user answers during the minigame
    public int answeredQuestions;

    private void OnEnable()
    {
        // subscribe function to the event
        GameManager.instance.gameFinished += clearData;
    }

    private void OnDisable()
    {
        GameManager.instance.gameFinished -= clearData;
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

    // clear questionsAnswered
    void clearData()
    {
        // function only runs if the right game mode is active
        if (GameManager.instance.gameState == GameManager.GameMode.FlashCards)
        {
            answeredQuestions = 0;
        }
    }

    // index = which button was selected. get the text of that button and compare it to answer
    public void checkAnswer(int index)
    {
        // get answer on button user selected
        string userInput = answerTexts[index].text;
        // get answer from questionBox
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
        GameManager.instance.NextQuestion();
    }
}
