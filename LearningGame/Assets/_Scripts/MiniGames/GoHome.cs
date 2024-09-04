using UnityEngine;


// This script is ran when a minigame ends. Just sends user back to home page.
public class GoHome : MonoBehaviour
{
    // Close current game page, Go back to home page.
    [SerializeField] GameObject FIB;
    [SerializeField] GameObject FlashCards;
    [SerializeField] GameObject Matching;
    [SerializeField] GameObject HomePage;

    private void Awake()
    {
        // Subscribe to gameFinished event to go home.
        GameManager.instance.gameFinished += NavigateHome;
    }

    // disables any minigame that might be running and sends user to home page
    public void NavigateHome()
    {
        HomePage.SetActive(true);

        if (FIB.activeInHierarchy == true)
        {
            FIB.SetActive(false);
        }
        if (FlashCards.activeInHierarchy == true)
        {
            FlashCards.SetActive(false);
        }
        if (Matching.activeInHierarchy == true)
        {
            Matching.SetActive(false);
        }

    }
}
