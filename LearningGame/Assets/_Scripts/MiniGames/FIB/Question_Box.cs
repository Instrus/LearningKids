using TMPro;
using UnityEngine;

public class Question_Box : MonoBehaviour
{
    // reference to the QuestionBox Text component (child)
    public TextMeshProUGUI questionBoxText;

    // pool of questions 
    public string[] questions;
    // pool of answers (must keep questions and answers in same order within Unity inspector)
    public string[] answers;
    // index to pair answer with question
    public int questionIndex;

    // on game object enabled, subscribe functions to events
    private void OnEnable()
    {
        // updates the GameManagers pool count because a different game mode might have a different # of questions
        GameManager.instance.poolCount = questions.Length;
        GameManager.instance.nextQuestion += RandomQuestion;
    }

    // On game object disable, unsubscribe from events
    private void OnDisable()
    {
        GameManager.instance.nextQuestion -= RandomQuestion;
    }

    private void Start()
    {
        // first random question upon start of minigame run
        GameManager.instance.NextQuestion();
    }

    // Get next question - x comes from GameManager in NextQuestion()
    public void RandomQuestion(int x)
    {
        string randomQuestion = questions[x];
        questionBoxText.text = randomQuestion;
        questionIndex = x;
    }
}
