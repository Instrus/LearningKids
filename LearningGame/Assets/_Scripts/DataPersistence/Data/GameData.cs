using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;

    // initial values when a new player plays the game for the first time (when no data to load)
    public GameData() {
        this.score = 0;
    }
}
