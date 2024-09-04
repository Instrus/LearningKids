using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    PlayerData playerData;

    public int points;
    TextMeshProUGUI pointsText;

    private float pitch = 1f;

    [SerializeField] private AudioClip incrementScoreSound;

    void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        // Gets TextMeshPro component
        pointsText = GetComponent<TextMeshProUGUI>();

        // subscribes at start of runtime. There will only be on score system so just call game managers events when needed.
        ExperimentalGM.instance.gameStarted += ResetPoints;
        ExperimentalGM.instance.scoreIncremented += IncrementScore;
        ExperimentalGM.instance.gameFinished += SendPoints;

    }
    // maybe convert OnEnable/Disable subscriptions later

    void ResetPoints()
    {
        points = 0;
        pointsText.text = "Score: " + points.ToString();
    }

    void IncrementScore()
    {
        StartCoroutine( DisplayPoints() );
    }

    IEnumerator DisplayPoints()
    {
        if (incrementScoreSound == null)
            yield return null;

        pitch = Random.Range(1f, 1.1f);

        for (int i = 0; i < 10; i++)
        {
            pointsText.text = "Score: " + (++points).ToString();
            AudioManager.instance.PlayClip( incrementScoreSound );
            AudioManager.instance.ChangePitch( pitch+=0.25f );
            yield return new WaitForSeconds(0.05f);
        }

    }

    // Updates PlayerDatas currency / score
    void SendPoints()
    {
        playerData.AddCurrency(points);
        playerData.AddScore(points);
    }

}
