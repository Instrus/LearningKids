using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class FIB_E : MonoBehaviour
{
    Card nextCard;
    [SerializeField] private GameObject thisPage; // reference to self (screen) to disable at end of game

    // ref to UI elements within FIB
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private Image questionBox; // to change color
    [SerializeField] private Button submitButton; // to disable between answers
    // figure out what to do with settings buttons later. maybe make a prefab of them and do a singleton

    [SerializeField] public int maxAnswers = 10;

    private int currentScore; //should find a way to integrate this with the score system

    public GameObject map;

    // health
    [SerializeField] public HealthManager playerHealth;
    [SerializeField] public HealthManager npcHealth;

    // characters
    [SerializeField] public GameObject playerPrefab;
    private GameObject player; // change to a prefab
    [SerializeField] public GameObject NPCprefab;
    private GameObject NPC1;
    [SerializeField] public Transform playerPos;
    [SerializeField] public Transform NPCpos;

    // colors
    Color32 color_correct = new Color32(0x70, 0xFF, 0x74, 0xFF);
    Color32 color_incorrect = new Color32(0xFF, 0x5D, 0x5D, 0xFF);

    // popup window
    [SerializeField] public GameObject popup;
    TextMeshProUGUI header;
    TextMeshProUGUI desc;

    private void OnEnable()
    {
        // boxing ding sound effect

        map.SetActive(true);
        submitButton.enabled = true;
        currentScore = 0;
        ExperimentalGM.instance.SetGameMode(ExperimentalGM.GameMode.FIB);
        ExperimentalGM.instance.StartGame(); // event call
        StartCoroutine(RequestCard(0f)); // request new card at start of game

        if (playerPrefab != null)
        {
            // instantiate player
            player = Instantiate(playerPrefab, playerPos.position, Quaternion.identity);
            player.transform.localScale = new Vector3(2, 2, 0);

            // instantiate NPC
            NPC1 = Instantiate(NPCprefab, NPCpos.position, Quaternion.identity);
            NPC1.transform.localScale = new Vector3(2, 2, 0);
        }

        //find GameObject named HealthManager - change to serializedfield player health (need npc health)
        playerHealth.InstantiateHearts(playerHealth.maxHealth);
        npcHealth.InstantiateHearts(npcHealth.maxHealth);

        // popup window components
        header = popup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        desc = popup.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); ;
    }

    // Popup called when enemy loses or player loses (health = 0) - called by HealthManager
    public void PopupWindow()
    {
        // depending if they win or lose change text

        if (popup != null)
        {
            popup.SetActive(true);
            header.text = "Nice job!";
            desc.text = "Score: " + currentScore + " / " + (maxAnswers * 10); // probably need to change this later. Not sure if we'll do max 10
        }
        
        // set header and desc according to win/lose
        // can use the popup window also to show the correct answer (dont do endgame at that point)
    }

    //ENDGAME() - popup will call EndGame on "OK"
    public void EndGame()
    {
        questionBox.color = Color.white;
        questionText.text = "";
        map.SetActive(false);
        Destroy(player);
        Destroy(NPC1);
        playerHealth.ClearHeartsList();
        npcHealth.ClearHeartsList();
        popup.SetActive(false);
        ExperimentalGM.instance.EndGame(); //event call
        ExperimentalGM.instance.GoHome(thisPage); // calls GM to close this page
    }

    // - request from GM database
    // change to request cards like in FlashCards to ensure unique. Should probably request all but make sure all are unique
    // dont forget PII card
    public IEnumerator RequestCard(float time)
    {
        yield return new WaitForSeconds(time);

        nextCard = ExperimentalGM.instance.GetRandomCard(); // should rename GM's function
        SetUIData();
    }

    // Sets UI elements to card question/answer
    private void SetUIData()
    {
        questionText.text = nextCard.question;
    }

    // Checks if user input satisfies the answer
    public void CheckAnswer()
    {

        StartCoroutine( DisableButton() );

        // should add some time to disable the button

        // have a pool of answers instead. search through them all on check

        if (userInput.text == "")
        {
            Debug.Log("Please enter text");
            return;
        } else if ( string.Equals(userInput.text, nextCard.answer, System.StringComparison.OrdinalIgnoreCase) )
        {
            StartCoroutine(FlashGreen());
            // increment score
            ExperimentalGM.instance.IncrementPoints();
            currentScore += 10;
            // damage NPC if wrong
            npcHealth.TakeDamage();
        }
        else {
            Debug.Log("answer is false");
            StartCoroutine(FlashRed());
            // damage player if wring
            playerHealth.TakeDamage();
        }

        // clear user input
        userInput.text = "";
        StartCoroutine(RequestCard(1f));
    }

    private IEnumerator DisableButton()
    {
        submitButton.enabled = false;

        yield return new WaitForSeconds(1f);

        submitButton.enabled = true;
    }

    // question box flashes green on answer correct
    private IEnumerator FlashGreen()
    {
        questionBox.color = color_correct;
        yield return new WaitForSeconds(1f);
        questionBox.color = Color.white;
    }

    // question box flashes red on answer incorrect
    private IEnumerator FlashRed()
    {
        questionBox.color = color_incorrect;
        yield return new WaitForSeconds(1f);
        questionBox.color = Color.white;
    }
}
