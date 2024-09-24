using TMPro;
using UnityEngine;

public class DisplayCurrency : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField] public TextMeshProUGUI currencyText;

    private void OnEnable()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        currencyText.text = playerData.GetCurrency().ToString() + "$";
    }

    // updates the currency text within the shop. When item is purchased, playerData currency altered also.
    public void UpdateCurrencyValue(int value) // use a positive number to increase, negative number to decrease
    {
        playerData.AddCurrency(value);
        currencyText.text = playerData.GetCurrency().ToString() + "$";
    }





}
