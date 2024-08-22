using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    private PlayerData playerData;

    private void Start()
    {
        // Get the PlayerData component
        playerData = FindObjectOfType<PlayerData>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData not found in the scene!");
            return;
        }

        // Set up the input field
        SetupInputField();
    }

    private void SetupInputField()
    {
        // Pre-populate the input field with the current username
        usernameInputField.text = playerData.GetUsername();

        // Add listener for when editing ends
        usernameInputField.onEndEdit.AddListener(OnUsernameInputEndEdit);
    }

    private void OnUsernameInputEndEdit(string newUsername)
    {
        // Set the new username when editing ends
        playerData.SetUsername(newUsername);
    }

    private void OnEnable()
    {
        // Refresh the input field text when the settings scene is activated
        if (playerData != null)
        {
            usernameInputField.text = playerData.GetUsername();
        }
    }
}