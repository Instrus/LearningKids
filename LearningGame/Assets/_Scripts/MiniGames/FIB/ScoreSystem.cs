using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int points;
    TextMeshProUGUI pointsText;

    void Start()
    {
        // Gets TextMeshPro component
        pointsText = GetComponent<TextMeshProUGUI>();

        // subscribes at start of runtime. There will only be on score system so just call game managers events when needed.
        GameManager.instance.gameStarted += ResetPoints;
        GameManager.instance.scoreIncremented += IncrementScore;
        GameManager.instance.gameFinished += SendPoints;

    }

    void ResetPoints()
    {
        points = 0;
        DisplayPoints();
    }

    void IncrementScore()
    {
        points += 10;
        DisplayPoints();
    }

    void DisplayPoints()
    {
        string result = "Score: " + points.ToString();
        pointsText.text = result;
    }

    // Updates PlayerDatas currency / score
    void SendPoints()
    {
        PlayerData.instance.incrementCurrency(points);
        PlayerData.instance.incrementScore(points);
    }

}
