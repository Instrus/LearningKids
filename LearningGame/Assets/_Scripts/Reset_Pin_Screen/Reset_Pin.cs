using TMPro;
using UnityEngine;

public class Reset_Pin : MonoBehaviour
{
    // input field
    PlayerData playerData;
    [SerializeField] public TMP_InputField input;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    public void ChangePin(){
        string input_text = input.text; //get input from box
        playerData.SetPIN(int.Parse(input_text));
        Debug.Log(input);
    }
}
