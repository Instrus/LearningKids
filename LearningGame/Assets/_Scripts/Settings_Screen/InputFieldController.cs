using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    private PlayerData playerData;

    // change PIN options
    [SerializeField] public GameObject changePinScreen;
    [SerializeField] public TMP_InputField currentPIN_Input;
    [SerializeField] public TMP_InputField newPIN_Input;
    [SerializeField] private GameObject incorrectCurrentPIN_Text;
    [SerializeField] private GameObject incorectNewPIN_Text;

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

    public void OpenChangePinPage()
    {
        changePinScreen.SetActive(true);
    }

    public void CloseChangePiNScreen()
    {
        changePinScreen.SetActive(false);
    }

    // called by submit button
    public void ChangePIN()
    {
        int oldPIN;
        int newPIN;

        bool oldPIN_check = false;
        bool newPIN_check = false;

        if (int.TryParse(currentPIN_Input.text, out oldPIN))
            oldPIN_check = true;

        if (oldPIN != playerData.GetPin())
        {
            oldPIN_check= false;
            incorrectCurrentPIN_Text.SetActive(true);
        }
            

        if (int.TryParse(newPIN_Input.text, out newPIN))
            newPIN_check= true;
        else
            incorectNewPIN_Text.SetActive(true);

        // set new pin
        if (newPIN_check == true && oldPIN_check == true)
        {
            playerData.SetPIN(newPIN);
            print("Changed pin");
            changePinScreen.SetActive(false);
        }
        else
        {
            print("Could not change pin");
        }
            

    }
}