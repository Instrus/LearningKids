using System.Collections;
using System.Collections.Generic;
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
        FIB_Manager.instance.gameStarted += ResetPoints;
        FIB_Manager.instance.scoreIncremented += IncrementScore;
        FIB_Manager.instance.gameFinished += SendPoints;
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
