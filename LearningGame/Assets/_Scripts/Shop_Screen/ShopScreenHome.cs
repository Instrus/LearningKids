using TMPro;
using UnityEngine;

public class ShopScreenHome : MonoBehaviour
{

    [SerializeField] GameObject currency;

    private void OnEnable()
    {
        try
        {
            currency.SetActive(true);
        }
        catch (System.Exception e) { 
            Debug.LogWarning("Error: currencyText null: " + e.Message);
        }
    }

    private void OnDisable()
    {
        try
        {
            currency.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error: currencyText null: " + e.Message);
        }
    }
}
