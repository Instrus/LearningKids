using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    // Analytics
    public TextMeshProUGUI rightAnswerPercentageText;
    public TextMeshProUGUI totalAnswersText;
    public TextMeshProUGUI gamesPlayedText;
    public TextMeshProUGUI creditsSpentText;

    // Weekly playtime bargraph
    public List<Image> bars;
    public List<TextMeshProUGUI> playtimeLabels;

    private Dictionary<string, float> playtimeDictionary;
    public PlayerData playerData;
    public PlaytimeTracker playtimeTracker;

    void Awake()
    {
        // fetch the required game objects
        playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            Debug.LogError("PlayerData not found in the scene");
        }
        playtimeTracker = FindObjectOfType<PlaytimeTracker>();
        if (playtimeTracker == null)
        {
            Debug.LogError("PlaytimeTracker not found in the scene");
        }

        PopulateAnalytics();
        RenderBarGraph();
    }

    public void PopulateAnalytics()
    {
        int rightAnswers = playerData.GetRightAnswers();
        int totalAnswers = playerData.GetTotalAnswers();

        // Prevent division by zero
        if (totalAnswers != 0)
        {
            // Calculate the percentage of right answers
            float answerPercentage = (float)rightAnswers / totalAnswers * 100;
            int answerPercentageRounded = Mathf.RoundToInt(answerPercentage);
            rightAnswerPercentageText.text = answerPercentageRounded.ToString() + "%";
        }
        else // 0 total answers
        {
            rightAnswerPercentageText.text = "N/A";
        }

        totalAnswersText.text = playerData.GetTotalAnswers().ToString();
        gamesPlayedText.text = playerData.GetGamesPlayed().ToString();
        creditsSpentText.text = playerData.GetCreditsSpent().ToString();
    }

    public void RenderBarGraph()
    {
        playtimeDictionary = playtimeTracker.GetSavedPlaytime();
        float[] playtimeArray = playtimeDictionary.Values.ToArray();

        for (int i = 0; i < playtimeArray.Length; i++)
        {
            // calculate and set the fillAmount for all bars
            bars[i].fillAmount = convertPlaytimeToFillAmount(playtimeArray[i]);

            // update the playtime labels
            playtimeLabels[i].text = Mathf.RoundToInt(playtimeArray[i]).ToString() + "m";
        }
    }

    private float convertPlaytimeToFillAmount(float playtime)
    {
        // set 60 minutes as the maximum bar height
        float fillAmount = playtime / 60;
        // clip the fill amount to 0-1
        if (fillAmount > 1) { fillAmount = 1; }
        if (fillAmount < 0) { fillAmount = 0; }
        return fillAmount;
    }
}
