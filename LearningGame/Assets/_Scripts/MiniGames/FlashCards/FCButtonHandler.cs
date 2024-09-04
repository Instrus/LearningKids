using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// FlashCards button handler
public class FCButtonHandler : MonoBehaviour
{
    private FlashCards_E fc;
    private Button button;
    private TextMeshProUGUI buttonText;

    private Color originalColor;

    [SerializeField] private AudioClip buttonSound;

    private void Awake()
    {
        // find FlashCard game object -> component
        fc = GameObject.Find("FlashCards").GetComponent<FlashCards_E>();
        button = GetComponent<Button>();
        // get button text component
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originalColor = button.image.color;
    }

    public void CallCheckAnswer() { StartCoroutine(CheckAnswer()); }

    public IEnumerator CheckAnswer()
    {
        if (buttonSound!= null)
        {
            AudioManager.instance.PlayClip(buttonSound);
        }

        // Call the CheckAnswer method from the FlashCards_E script
        StartCoroutine(fc.CheckAnswer(buttonText.text));

        // Manually set the pressed color
        var colors = button.colors;
        button.image.color = colors.pressedColor;

        // disable buttons for 1 second
        button.enabled = false; //need to disable all buttons
        yield return new WaitForSeconds(0.6f);

        // Reset the color to its original state
        button.image.color = originalColor;
        button.enabled = true;
    }
}
