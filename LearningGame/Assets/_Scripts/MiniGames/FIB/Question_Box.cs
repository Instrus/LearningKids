using TMPro;
using UnityEngine;


// can add an optional answer set (2d array, 2nd element = 4) for FlashCards. Need a function to pair it with.
// the function will be called from FlashCardsAnswerSet though.
// needs to be a custom class or something. look at a tutorial. Something about a serializable class

public class Question_Box : MonoBehaviour
{
    // Reference to the Answer Box Text
    public TextMeshProUGUI text;

    // Pool of questions 
    public string[] questions;
    // Pool of answers (Keep in same order in the inspector)
    public string[] answers;
    
    // index to pair answer with question
    public int questionIndex;

    // On game object enabled, subscribe events
    private void OnEnable()
    {
        // Updates the GameManagers pool count
        GameManager.instance.currentPoolCount = questions.Length; // may need to make this an event instead as different games have different counts?

        GameManager.instance.nextQuestion += RandomQuestion;
        //GameManager.instance.gameStarted += UpdatePoolCount;
    }

    // On game object disable, unsubscribe from events
    private void OnDisable()
    {
        GameManager.instance.nextQuestion -= RandomQuestion;
        //GameManager.instance.gameStarted -= UpdatePoolCount;
    }

    private void Start()
    {
        GameManager.instance.NextQuestion(); //Event call
        
    }

    // Methods:

    // Get next question (random selection) (can be changed to alter question or something like that. The real random question happens in the game manager.
    public void RandomQuestion(int x)
    {
        string randomQuestion = questions[x];
        text.text = randomQuestion;
        questionIndex = x;
    }

    // Updates GameManagers poolCount.
    //public void UpdatePoolCount()
    //{
    //    print("Updated pool count!");
    //    GameManager.instance.currentPoolCount = questions.Length;
    //}
}
