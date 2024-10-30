using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeTracker : MonoBehaviour
{
    public Dictionary<string, float> playtimePerDay; // 7-day playtime, key is the day of the week
    private string[] daysOfTheWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private string weekStartDate; // Always the most recent Monday of the week
    private DateTime sessionStartTime;

    void Start()
    {
        sessionStartTime = DateTime.Now; // Begin the session timer
        playtimePerDay = GetSavedPlaytime(); // Load the saved playtime
        weekStartDate = GetWeekStartDate(); // Calculate Monday's date this week

        // New week or new user. Update PlayerPrefs and reset playtime
        if (!PlayerPrefs.HasKey(weekStartDate))
        {
            PlayerPrefs.SetString(weekStartDate, "WeekStartDate");
            PlayerPrefs.Save();
            ResetPlaytime();
        }
    }

    void OnApplicationQuit()
    {
        SavePlaytime();
    }

    // Stop the play timer when the application is paused
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SavePlaytime();
        }
        else
        {
            sessionStartTime = DateTime.Now;
        }
    }

    public void SavePlaytime()
    {
        // Calculate the duration of the session
        TimeSpan sessionDuration = DateTime.Now - sessionStartTime;
        sessionStartTime = DateTime.Now; // Fix overlapping playtime

        string today = DateTime.Now.DayOfWeek.ToString();

        // Ensure the dictionary and playerprefs keys exist
        if (playtimePerDay.ContainsKey(today) && PlayerPrefs.HasKey(today))
        {
            // Update today's playtime
            playtimePerDay[today] += (float)sessionDuration.TotalMinutes;

            // Save the weekly playtime and week start date
            PlayerPrefs.SetFloat(today, playtimePerDay[today]);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError(today + " is not a valid key in " + playtimePerDay + " or PlayerPrefs.");
        }
    }

    public void ResetPlaytime()
    {
        if (PlayerPrefs.HasKey("Monday"))
        {
            for (int i = 0; i < 7; i++)
            {
                PlayerPrefs.SetFloat(daysOfTheWeek[i], 0f);
            }
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Can't reset playtime: Monday doesn't exist in PlayerPrefs.");
        }
    }

    // Get each day's playtime from PlayerPrefs and initialize it for new users
    public Dictionary<string, float> GetSavedPlaytime()
    {
        playtimePerDay = new Dictionary<string, float>();

        // Initialize the playtime in PlayerPrefs for new users
        if (!PlayerPrefs.HasKey("Monday"))
        {
            for (int i = 0; i < 7; i++)
            {
                PlayerPrefs.SetFloat(daysOfTheWeek[i], 0f);
            }
            PlayerPrefs.Save();
        }

        // Load the saved playtime
        for (int i = 0; i < 7; i++)
        {
            string day = daysOfTheWeek[i];
            float savedPlaytime = PlayerPrefs.GetFloat(day);
            playtimePerDay.Add(day, savedPlaytime);
        }

        return playtimePerDay;
    }

    // Calculate what monday's date was
    public string GetWeekStartDate()
    {
        DateTime todaysDate = DateTime.Now.Date;
        int daysSinceMonday = ((int)todaysDate.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        // Backtrack to find the date of the most recent Monday
        return todaysDate.AddDays(-daysSinceMonday).ToString("yyyy-MM-dd");
    }

    public Dictionary<string, float> GetPlaytimeDictionary()
    {
        return playtimePerDay;
    }
}
