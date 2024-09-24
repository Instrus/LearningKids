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
        // instantiates cardDatas in each shop screen (avatar, clothes, hats) - not intended for Shop_Screen
        int index = 0;
        foreach (var data in cardDatas)
        {
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
