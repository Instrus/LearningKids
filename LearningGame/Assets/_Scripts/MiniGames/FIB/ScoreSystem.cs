using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    PlayerData playerData;

    public int points;
    TextMeshProUGUI pointsText;

    void Awake()
    {

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        // Gets TextMeshPro component
        pointsText = GetComponent<TextMeshProUGUI>();

        // subscribes at start of runtime. There will only be on score system so just call game managers events when needed.
        GameManager.instance.gameStarted += ResetPoints;
        GameManager.instance.scoreIncremented += IncrementScore;
        GameManager.instance.gameFinished += SendPoints;

        ExperimentalGM.instance.gameStarted += ResetPoints;
        ExperimentalGM.instance.scoreIncremented += IncrementScore;
        ExperimentalGM.instance.gameFinished += SendPoints;

    }
    // maybe convert OnEnable/Disable subscriptions later

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
        playerData.AddCurrency(points);
        playerData.AddScore(points);
    }

}
