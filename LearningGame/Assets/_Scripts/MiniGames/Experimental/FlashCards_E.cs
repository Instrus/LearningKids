using TMPro;
using UnityEngine;

public class FlashCards_E : MonoBehaviour
{
    private Card nextCard;
    [SerializeField] private GameObject thisPage; // reference to self (screen) to disable at end of game

    // for populating the text fields
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI button1;
    [SerializeField] TextMeshProUGUI button2;
    [SerializeField] TextMeshProUGUI button3;
    [SerializeField] TextMeshProUGUI button4;

    public int answerCount = 0; // number of questions player has answered.

    private int currentScore; // keeps track of current earned points (remove later)

    private void OnEnable()
    {
        currentScore = 0;
        ExperimentalGM.instance.SetGameMode(ExperimentalGM.GameMode.FlashCards); // set GameManager mode to FlashCards
        ExperimentalGM.instance.StartGame(); // event call
        RequestCard(); // request card from start
    }

    private void EndGame()
    {
        ExperimentalGM.instance.EndGame(); //event call
        ExperimentalGM.instance.GoHome(thisPage); // calls GM to close this page
    }

    // next question - request from GameManager
    public void RequestCard()
    {
        if (answerCount < 5) {
            nextCard = ExperimentalGM.instance.GetRandomCard();
            SetUIElementsData();
        }
    }

    // Sets the UI elements of FlashCards to the next cards questions/answer set
    private void SetUIElementsData()
    {
        // set question text
        questionText.text = nextCard.question;
        // set button texts
        button1.text = nextCard.answerSet[0];
        button2.text = nextCard.answerSet[1];
        button3.text = nextCard.answerSet[2];
        button4.text = nextCard.answerSet[3];
    }

    // Check if user input equals the Cards answer
    public void CheckAnswer(string buttonText)
    {
        if (buttonText == null)
        {
            return;
        } else if (buttonText == nextCard.answer)
        {
            ExperimentalGM.instance.IncrementPoints();
            Debug.Log("answer is correct!");
            currentScore += 10;
        }
        else
        {
            Debug.Log("answer is false");
        }

        ExperimentalGM.instance.IncrementPoints();

        IncrementAnswerCount();
        RequestCard();
    }

    public void IncrementAnswerCount()
    {
        answerCount++;
        CheckAnswerCount();
    }

    public void CheckAnswerCount()
    {
        if (answerCount >= 5)
        {
            print("Game over");
            EndGame();
        }
    }

}
