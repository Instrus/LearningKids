using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Prefab stored in resources to be called by each shop card button.

public class PurchaseCosmetic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] Image cosmeticImage;
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;

    [SerializeField] AudioClip purchaseClip;

    // initialize values upon instantiating from where instantiation occurs.
    public ShopCardData shopCardData;

    PlayerData playerData;

    private void Start() { 
        headerText.text = "Do you want to buy the " + shopCardData.cosmeticName + " for $" + shopCardData.price + "?";
        cosmeticImage.sprite = shopCardData.cosmeticImage;

        try
        {
            playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        } catch(Exception e)
        {
            Debug.LogError("PurchaseCosmetic: Could not find PlayerData" + e.Message);
        }
        
    }

    // called by "Yes" button
    public void ConfirmPurchase() {

        bool verification = VerifyCurrency();
        
        if (verification)
        {
            // analytics
            playerData.AddCreditsSpent(shopCardData.price);

            AddCosmeticToPlayer();
            if (purchaseClip != null)
                AudioManager.instance.PlayClip(purchaseClip);

            GameObject.Find("Currency").GetComponent<DisplayCurrency>().UpdateCurrencyValue(-shopCardData.price);

            // remove scriptable object from list in the shop it came from.
            RemoveItemFromShop();
            Destroy(gameObject);
        }
        else
        {
            headerText.text = "Sorry, not enough money. Play games to win more!";
        }

    }

    // called by "No" button
    public void CancelPurchase() { Destroy(gameObject); }

    // verifies if user has enough money
    public bool VerifyCurrency()
    {
        int userCurrency = playerData.GetCurrency();

        if (userCurrency >= shopCardData.price) return true;
        else return false;
    }

    public void AddCosmeticToPlayer()
    {
        if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Avatar) { playerData.unlockedAvatarIDs.Add(shopCardData.cosmeticID); }
        else if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Clothes) { playerData.unlockedClothesIDs.Add(shopCardData.cosmeticID); }
        else if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Hat) { playerData.unlockedHatsIDs.Add(shopCardData.cosmeticID); }
        else { Debug.LogWarning("Error - PurchaseCosmetic: Cannot add cosmetic to player. Type does not exist."); }
    }

    public void RemoveItemFromShop()
    {
        // removes item by name and ID from shop (as child of grid)
        if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Avatar) {
            GameObject.Find("Avatar_Shop").GetComponent<ShopScreen>().RemoveItem(shopCardData.cosmeticID, shopCardData.cosmeticName);
        }
        else if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Clothes) {
            GameObject.Find("Clothes_Shop").GetComponent<ShopScreen>().RemoveItem(shopCardData.cosmeticID, shopCardData.cosmeticName);
        }
        else if (shopCardData.cosmeticType == ShopCardData.CosmeticType.Hat) {
            GameObject.Find("Hats_Shop").GetComponent<ShopScreen>().RemoveItem(shopCardData.cosmeticID, shopCardData.cosmeticName);
        }
        else { Debug.LogWarning("Error - RemoveFromShop: Type does not exist."); }
    }

}
