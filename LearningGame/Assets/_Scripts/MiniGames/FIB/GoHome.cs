using UnityEngine;

public class GoHome : MonoBehaviour
{
    // Close current game page, Go back to home page.
    [SerializeField] GameObject FIB;
    [SerializeField] GameObject FlashCards;
    [SerializeField] GameObject Matching;
    [SerializeField] GameObject HomePage;

    // problem: GoHome tries to run after any game. Right now the MiniGamePage only references FIB. Need to make it work for the others. 
    // Note: It's attached to GameManager so I'd probably need other conditions. Like depending on game manager state, close that certain page. etc.

    private void Start()
    {
        // Subscribe to gameFinished event to go home.
        GameManager.instance.gameFinished += navigateHome;
    }

    public void navigateHome()
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
