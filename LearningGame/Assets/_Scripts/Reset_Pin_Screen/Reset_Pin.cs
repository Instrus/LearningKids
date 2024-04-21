using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reset_Pin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_InputField input;

    public void ChangePin(){

        string input_text = input.text; //get input from box
        PlayerData.instance.setPin(int.Parse(input_text));
        //Debug.Log(input);
        
    }
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
