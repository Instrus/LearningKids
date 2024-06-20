using TMPro;
using UnityEngine;

// FlashCards button handler
public class FCButtonHandler : MonoBehaviour
{
    private FlashCards_E fc;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        // find FlashCard game object -> component
        fc = GameObject.Find("FlashCards (experimental)").GetComponent<FlashCards_E>();
        // get button text component
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void CallCheckAnswer() { fc.CheckAnswer(buttonText.text); }
}
