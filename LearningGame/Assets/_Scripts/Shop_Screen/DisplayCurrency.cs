using TMPro;
using UnityEngine;

public class DisplayCurrency : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField] public TextMeshProUGUI currencyText;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    private void Update()
    {
        int value = playerData.GetCurrency();
        currencyText.text = ("$" + value.ToString());
    }



}
