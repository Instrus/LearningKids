using UnityEngine;
using TMPro;
using System;

public class CreateAccount : MonoBehaviour
{
    PlayerData playerData;

    // Input fields
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField PINinput;

    // Screen navigation
    [SerializeField] private GameObject PinScreen;
    [SerializeField] private GameObject CreateAccountScreen;

    // Error messages
    [SerializeField] private GameObject incorrectUsernameErrorText;
    [SerializeField] private GameObject incorrectPinErrorText;

    // New user data
    private string newUserName;
    private int newPIN;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        newUserName = string.Empty;
        newPIN = -1;
    }

    public void CreateNewAccount()
    {
        incorrectUsernameErrorText.SetActive(false);
        incorrectPinErrorText.SetActive(false);

        // Validate username input
        newUserName = userNameInput.text;
        if (string.IsNullOrEmpty(newUserName))
        {
            incorrectUsernameErrorText.SetActive(true);
            Debug.Log("Username cannot be empty.");
            return;
        }

        // Validate PIN input
        if (!int.TryParse(PINinput.text, out newPIN))
        {
            incorrectPinErrorText.SetActive(true);
            Debug.Log("PIN must be a valid integer.");
            return;
        }

        // Ensure PIN is exactly 4 digits long
        if (PINinput.text.Length != 4)
        {
            incorrectPinErrorText.SetActive(true);
            Debug.Log("PIN must be exactly 4 digits long.");
            return;
        }

        // Set user data
        try
        {
            playerData.SetUsername(newUserName);
            playerData.SetPIN(newPIN);
        }
        catch (Exception e)
        {
            Debug.LogError("Error setting user data: " + e.Message);
            return;
        }

        Debug.Log("New account created: " + newUserName + " " + newPIN);
        ReturnToPinScreen();
    }

    // Function to return to the login screen
    private void ReturnToPinScreen()
    {
        // clear input fields
        userNameInput.text = "";
        PINinput.text = "";

        // return to pin screen
        PinScreen.SetActive(true);
        CreateAccountScreen.SetActive(false);
    }
}
