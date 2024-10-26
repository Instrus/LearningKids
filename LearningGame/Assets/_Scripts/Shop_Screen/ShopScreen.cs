using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{

    [SerializeField] public GameObject shopCardPrefab;
    [SerializeField] public List<ShopCardData> cardDatas; //(this is the items the shop will hold to sell)
    [SerializeField] public GameObject grid;

    private void Start()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        // instantiates cardDatas in each shop screen (avatar, clothes, hats) - not intended for Shop_Screen
        int index = 0;
        foreach (var data in cardDatas)
        {


            if (data.cosmeticType == ShopCardData.CosmeticType.Avatar)
            {
                if (playerData.unlockedAvatarIDs.Contains(data.cosmeticID))
                {
                    return;
                }
            }
            else if (data.cosmeticType == ShopCardData.CosmeticType.Hat)
            {
                if (playerData.unlockedHatsIDs.Contains(data.cosmeticID))
                {
                    return;
                }
            } else if (data.cosmeticType == ShopCardData.CosmeticType.Clothes)
            {
                if (playerData.unlockedClothesIDs.Contains(data.cosmeticID))
                {
                    return;
                }
            }

            GameObject newCard = Instantiate(shopCardPrefab, grid.transform);
            //newCard.GetComponent<ShopCard>().cardImage.sprite = data.cosmeticImage;
            newCard.transform.GetChild(0).GetComponent<ShopCard>().shopCardData = data;

            newCard.name = (cardDatas[index].cosmeticName + "Card");

            index++;
        }
    }

    // removes the scriptable object cardData and the physical card from the shop.
    // not persistent. Need to save to a json file.
    // might just have to save the information to playerData (cards left to purchase)
    public void RemoveItem(int ID, string cosmeticName) { 
        cardDatas.RemoveAll(c => c.cosmeticID == ID);
        Destroy(GameObject.Find(cosmeticName + "Card"));
    }

}
