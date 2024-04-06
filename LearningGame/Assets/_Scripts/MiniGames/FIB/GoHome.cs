using UnityEngine;

public class GoHome : MonoBehaviour
{
    // Close current game page, Go back to home page.
    [SerializeField] GameObject MiniGamePage;
    [SerializeField] GameObject HomePage;

    private void Start()
    {
        // Subscribe to gameFinished event to go home.
        GameManager.instance.gameFinished += navigateHome;
    }

    public void navigateHome()
    {
        HomePage.SetActive(true);
        MiniGamePage.SetActive(false);
    }
}
