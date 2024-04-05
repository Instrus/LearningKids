using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    // AppManager needs to handle most of the apps functions. Acts as a bridge.

    // gameFinished subscription:
    // Functions: (F1): Fetch score from PlayerData, (F2): Send score to scoresPage (will need a function for scoresPage to call)
    // Functions: (F1): Fetch currency value from PlayerData. (F2): Update shop text (available currency value?) (will need a function for shopPage to call)

    void Start()
    {
        // subscribe to events. (all games)
        FIB_Manager.instance.gameFinished += fetchCurrency;
        FIB_Manager.instance.gameFinished += fetchCurrency;
        // 
    }

    // Current problem. This seems to be pointless. Since PlayerData is actually a singleton, we can always just get the information from there automatically.
    // Perhaps whenever the scoreboard page is opened, there needs to be some function ran (event) that automatically gets the information from

    public void fetchCurrency()
    {
        // store value, call postCurrency
        int currency = PlayerData.instance.getCurrency();
        Debug.Log(currency);
        postCurrency(currency);
    }

    public void fetchScore()
    {
        // store score, call postScore
        int score = PlayerData.instance.getScore();
        Debug.Log(score);
        postScore(score);
    }

    // Updates 
    public void postCurrency(int value)
    {
        // call function in shop page, pass value
    }

    public void postScore(int value)
    {
        // call function in score page, pass value
    }
}
