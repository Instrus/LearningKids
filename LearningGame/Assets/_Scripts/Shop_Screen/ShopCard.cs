using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI element for shop.

public class ShopCard : MonoBehaviour
{
    [SerializeField] public ShopCardData shopCardData;
    [SerializeField] public Image cardImage;

    private void Start()
    {
        cardImage.sprite = shopCardData.cosmeticImage;
    }

    // when a shop card button is pressed, this function is called
    public void PurchaseCosmetic()
    {
        GameObject purchasePage = Instantiate(Resources.Load("ShopConfirmationPage") as GameObject, GameObject.Find("Canvas").transform);
        purchasePage.GetComponent<PurchaseCosmetic>().shopCardData = shopCardData;
    }
}
