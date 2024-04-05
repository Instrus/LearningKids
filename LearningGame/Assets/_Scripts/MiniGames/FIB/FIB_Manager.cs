using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System; // Event system

public class FIB_Manager : MonoBehaviour
{

    // Singleton
    public static FIB_Manager instance { get; private set; }
    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Either delegates or events. Containers for methods.
    public event Action gameStarted, gameFinished, scoreIncremented;

    // Need to reference PlayerData?

    // Call this function from the button that opens the minigame (selection page)
    public void StartGame()
    {
        Debug.Log("Game started!");
        gameStarted?.Invoke();
    }

    // increments users points by 10 upon successful answer given.
    public void IncrementPoints()
    {
        scoreIncremented?.Invoke();
    }

    public void EndGame()
    {
        gameFinished?.Invoke();

        // (Maybe the PlayerData script needs to subscribe some functions to this event.)

        // send points back to user as currency
        
        // activate home page
        
        // reset all values for future game?

        // close page
    }
}


// Note, I can create a function that runs when the submit button is pressed. It can run the other events.
// Should make a separate score script that subscribes to this game manager