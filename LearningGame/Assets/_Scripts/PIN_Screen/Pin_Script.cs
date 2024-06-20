using TMPro;
using UnityEngine;

public class Pin_Screen_Script : MonoBehaviour
{
    [SerializeField] public TMP_InputField input; // input field
    [SerializeField] GameObject PinScreen;
    [SerializeField] GameObject HomePage;
    [SerializeField] GameObject incorrect_text;

    PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    public void submitCheck(){

        //get input from box
        string input_text = input.text; 

        // gets players current set PIN
        int user_pin = playerData.GetPin();

        if(input_text == user_pin.ToString()){
            HomePage.SetActive(true);
            PinScreen.SetActive(false);
            incorrect_text.SetActive(false);
        }
        else{
            incorrect_text.SetActive(true);
        }
        
    }

}
