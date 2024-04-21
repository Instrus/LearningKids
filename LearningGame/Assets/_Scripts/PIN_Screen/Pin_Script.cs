using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pin_Screen_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_InputField input;
     [SerializeField] GameObject PinScreen;
    [SerializeField] GameObject HomePage;
    [SerializeField] GameObject incorrect_text;
    public void submitCheck(){

        string input_text = input.text; //get input from box
        //Debug.Log(input);
        int user_pin = PlayerData.instance.getPin();
        //Debug.Log(user_pin);
        if(input_text == user_pin.ToString()){
             HomePage.SetActive(true);
             PinScreen.SetActive(false);
            incorrect_text.SetActive(false);
        }
        else{
            incorrect_text.SetActive(true);
        }
        
    }
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
