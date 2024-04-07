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

    // length to make sure there is at least one question in the pool
    int length = 0;
    private void Start()
    {
        length = questions.Length;
        RandomQuestion();
    }

    // Methods:

    // Get next question (random selection)
    public void RandomQuestion()
    {
        // if there is a question in the pool,
        if (length > 0)
        {
            int randomIndex = Random.Range(0, length);

            string randomQuestion = questions[randomIndex];

            // Update text
            text.text = randomQuestion;
            // get index
            questionIndex = randomIndex;
        }

    }
}
