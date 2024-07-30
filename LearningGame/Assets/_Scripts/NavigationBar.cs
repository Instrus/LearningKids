using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    // reference to all buttons of nav bar
    // reference to all related screens

    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject settingsScreen;
    //[SerializeField] GameObject profileScreen; // future implement

    // Maybe this script will be housed on the navbar, but the buttons will call functions from here.

    // If the user tries clicking a button (like settings) and its already open, do nothing, and so on

    // If the user tries to click on the home button, and is in settings, allow change.

    // potential: user clicks on home, all other  screens deactivated, and so forth.

    // Home button will call this function
    public void GoHome()
    {
        if (!homeScreen.activeInHierarchy) homeScreen.SetActive(true);

        if (settingsScreen.activeInHierarchy) settingsScreen.SetActive(false);
        //if (profileScreen.activeInHierarchy) profileScreen.SetActive(false);
    }

    public void GoSettings()
    {
        if(!settingsScreen.activeInHierarchy) settingsScreen.SetActive(true);

        if (homeScreen.activeInHierarchy) homeScreen.SetActive(false);
        //if (profileScreen.activeInHierarchy) profileScreen.SetActive(false);
    }

}
