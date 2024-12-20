using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Unity.UI;
using System.Diagnostics;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FlashCards_E : MonoBehaviour
{
    [SerializeField] private GameObject thisPage; // reference to self (screen) to disable at end of game

    // GUI (text)
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI button1;
    [SerializeField] TextMeshProUGUI button2;
    [SerializeField] TextMeshProUGUI button3;
    [SerializeField] TextMeshProUGUI button4;
    [SerializeField] TextMeshProUGUI answerCountText;

    // button components (to enable/disable)
    [SerializeField] Button[] buttons;

    // prefabs - visual feedback
    [SerializeField] GameObject check;
    [SerializeField] GameObject x;

    // DATA
    private Card card;
    private Card[] cards = new Card[10]; // card pool to ensure each question is unique per game session
    private int currentCardIndex = 0; // increment from first to last card
    private int currentScore; // keeps track of current earned points
    public int answerCount = 0; // number of correct questions player has answered.
    [SerializeField] private int maxAnswers;

    // MAP / BOUNDS
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject[] tracks = new GameObject[3]; //maybe make an array later
    private float trackIncrements;
    private Vector2 playerTrackBounds;
    private Vector2 NPC1TrackBounds;
    private Vector2 NPC2TrackBounds;

    // CHARACTERS
    [SerializeField] public GameObject playerPrefab;
    private GameObject player; // change to a prefab
    [SerializeField] public GameObject NPCprefab;
    private GameObject NPC1;
    private GameObject NPC2;
    private int NPC1_Score = 0;
    private int NPC2_Score = 0;

    // CHARACTER MOVEMENT
    private bool movePlayer = false;
    private bool moveNPC1 = false;
    private bool moveNPC2 = false;
    private Vector3 avatarTargetPosition;
    private Vector3 NPC1TargetPosition;
    private Vector3 NPC2TargetPosition;

    public bool coroutineRunning = false;

    // audio clips
    [SerializeField] AudioClip whistle;

    public GameObject popupWindow; // pops up menu when game over

    private PlayerData playerData;

    private void OnEnable()
    {
        // fetch playerdata for analytics
        playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            UnityEngine.Debug.LogError("PlayerData not found in scene");
        }

        // request random cards from card database, no dupes.
        RequestCards(maxAnswers, ExperimentalGM.instance.cardDB.FlashCards.Length);
        // requesting 10 cards out of all available cards (cardDB needs at least 10 cards to work properly)

        currentCardIndex = 0;
        currentScore = 0;
        NPC1_Score = 0;
        NPC2_Score = 0;
        answerCount = 0;
        map.SetActive(true);
        answerCountText.text = answerCount.ToString() + " / " + maxAnswers;

        // enable buttons
        foreach (var button in buttons)
            button.enabled = true;

        ExperimentalGM.instance.SetGameMode(ExperimentalGM.GameMode.FlashCards); // set GameManager mode to FlashCards
        ExperimentalGM.instance.StartGame(); // event call
        //RequestCard(); // initial card request
        NextCard();

        // track boundaries
        playerTrackBounds = new Vector3(tracks[0].GetComponent<Collider2D>().bounds.size.x,
                  tracks[0].GetComponent<Collider2D>().bounds.size.y);
        trackIncrements = playerTrackBounds.x / maxAnswers;
        Vector2 startPosition = new Vector2 (playerTrackBounds.x /2, playerTrackBounds.y);
        Vector3 leftmostPoint = tracks[0].GetComponent<Collider2D>().bounds.min;
        float leftmostX = tracks[0].GetComponent<Collider2D>().bounds.min.x;

        if (playerPrefab != null)
        {
            // instantiate player
            player = Instantiate(playerPrefab, leftmostPoint, Quaternion.identity);
            player.transform.position = new Vector2(leftmostX, tracks[0].transform.position.y + 0.3f);

            // instantiate NPCs
            NPC1 = Instantiate(NPCprefab, leftmostPoint, Quaternion.identity);
            NPC1.transform.position = new Vector2(leftmostX, tracks[1].transform.position.y + 0.3f);
            NPC2 = Instantiate(NPCprefab, leftmostPoint, Quaternion.identity);
            NPC2.transform.position = new Vector2(leftmostX, tracks[2].transform.position.y + 0.3f);

        }
    }

    private void Update()
    {
        // player movement
        if (movePlayer)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, avatarTargetPosition, 2 * Time.deltaTime);
            if (player.transform.position == avatarTargetPosition)
                movePlayer = false;
        }

        // NPC movement
        if (moveNPC1)
        {
            NPC1.transform.position = Vector3.MoveTowards(NPC1.transform.position, NPC1TargetPosition, 2 * Time.deltaTime);
            if (NPC1.transform.position == NPC1TargetPosition)
                moveNPC1 = false;
        }
        if (moveNPC2)
        {
            NPC2.transform.position = Vector3.MoveTowards(NPC2.transform.position, NPC2TargetPosition, 2 * Time.deltaTime);
            if (NPC2.transform.position == NPC2TargetPosition)
                moveNPC2 = false;
        }

    }

    // Sets the UI elements of FlashCards to the next cards questions/answer set
    private void SetUIElementsData()
    {
        // set question text
        questionText.text = card.question;
        // set button texts
        button1.text = card.answerSet[0];
        button2.text = card.answerSet[1];
        button3.text = card.answerSet[2];
        button4.text = card.answerSet[3];

        //answerCountText.text = answerCount.ToString() + " / " + maxAnswers.ToString();
    }

    public void RequestCards(int size, int range)
    {
        System.Random rand = new System.Random();
        HashSet<int> numbers = new HashSet<int>();

        // get random but unique numbers to request cards with
        while (numbers.Count < size)
        {
            int randomNumber = rand.Next(0, range);
            numbers.Add(randomNumber); // HashSet ensures no duplicates
        }

        // populate card pool
        int tempIndex = 0;
        foreach (int number in numbers) {
            //cards[tempIndex++] = ExperimentalGM.instance.cardDB.FlashCards[number];
            cards[tempIndex++] = ExperimentalGM.instance.cardDB.RequestFlashCards(number);
        }

    }

    public void NextCard()
    {
        if (currentCardIndex < cards.Length)
        {
            card = cards[currentCardIndex];
            SetUIElementsData();
            currentCardIndex++;
        }
    }

    // called by the popup window button to end game
    public void EndGame()
    {
        Destroy(player);
        Destroy(NPC1);
        Destroy(NPC2);

        popupWindow.SetActive(false);
        map.SetActive(false);

        ExperimentalGM.instance.EndGame(); //event call
        ExperimentalGM.instance.GoHome(thisPage); // calls GM to close this page
    }

    // Check if user input equals the Cards answer
    public IEnumerator CheckAnswer(string buttonText)
    {
        playerData.IncrementTotalAnswers(); // increment total answers for analytics

        // disable buttons for a time
        foreach (var button in buttons)
            button.enabled = false;

        // cap check
        if (answerCount < maxAnswers)
        {
            
            // no answer associated with button error
            if (buttonText == null)
            {
                print("Flashcards error: button invalid");
                yield return null;
            }

            else if (string.Equals(buttonText, card.answer, StringComparison.OrdinalIgnoreCase))
            {
                Instantiate(check, player.transform.position, Quaternion.identity); // add enabled VFX check (currently bugged if vsync not on)

                ExperimentalGM.instance.IncrementPoints();
                currentScore += 10;

                // move avatar
                avatarTargetPosition = new Vector2(player.transform.position.x + trackIncrements, tracks[0].transform.position.y + 0.3f);
                movePlayer = true;
            }
            else
            {
                Instantiate(x, player.transform.position, Quaternion.identity); // add enabled VFX check
            }

            // NPC movement
            if (NPCRoll() == 1 && NPC1_Score < 100)
            { // based on random number generation rather than NPCs guessing the real answers
                Instantiate(check, NPC1.transform.position, Quaternion.identity); // add enabled VFX check
                NPC1TargetPosition = new Vector2(NPC1.transform.position.x + trackIncrements, tracks[1].transform.position.y + 0.3f);
                NPC1_Score += 10;
                moveNPC1 = true;
            }
            else if (NPC1_Score < 100)
            {
                Instantiate(x, NPC1.transform.position, Quaternion.identity); // add enabled VFX check
            }
            if (NPCRoll() == 1 && NPC2_Score < 100)
            {
                Instantiate(check, NPC2.transform.position, Quaternion.identity); // add enabled VFX check
                NPC2TargetPosition = new Vector2(NPC2.transform.position.x + trackIncrements, tracks[2].transform.position.y + 0.3f);
                NPC2_Score += 10;
                moveNPC2 = true;
            }
            else if (NPC2_Score < 100)
            {
                Instantiate(x, NPC2.transform.position, Quaternion.identity); // add enabled VFX check
            }

            answerCount++;
            StartCoroutine(CheckAnswerCount());
            answerCountText.text = answerCount.ToString() + " / " + maxAnswers.ToString();

            if (answerCount < maxAnswers)
            {
                // request a new question
                NextCard();

                yield return new WaitForSeconds(0.5f);
                foreach (var button in buttons)
                    button.enabled = true;
            }
            else
            {
                if (whistle != null)
                {
                    yield return new WaitForSeconds(0.6f);
                    AudioManager.instance.PlayClip(whistle);
                }       
            }
        }
    }

    // checks if user answered max amount of questions
    public IEnumerator CheckAnswerCount()
    {
        if (answerCount >= maxAnswers)
        {

            

            // disable buttons
            foreach (var button in buttons)
                button.enabled = false;

            // wait for seconds
            yield return new WaitForSeconds(2f);

            // show game over results
            popupWindow.SetActive(true);
            GameObject popup = popupWindow.transform.GetChild(0).gameObject;
            popup.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore + " / " + (maxAnswers*10);
        }
        yield return null;
    }

    // random number generator for NPCs
    public int NPCRoll()
    {
        return UnityEngine.Random.Range(0, 2);
    }

}

// maybe dont check if someone wins, just wait for player to get all answers right. if npc wins, just stop movement
// might want to add 1st place, 2nd place, etc.
