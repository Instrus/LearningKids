using TMPro;
using UnityEngine;

public class DisplayCurrency : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI currencyText;


    private void Update()
    {
        int value = PlayerData.instance.getCurrency();
        currencyText.text = ("$" + value.ToString());
    }



}
