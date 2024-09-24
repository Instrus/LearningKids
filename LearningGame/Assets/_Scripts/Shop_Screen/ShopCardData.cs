using UnityEngine;

// scriptableobject

[CreateAssetMenu]
public class ShopCardData : ScriptableObject
{
    public enum CosmeticType { Avatar, Clothes, Hat}

    [SerializeField] public CosmeticType cosmeticType;
    [SerializeField] public int price = 0;
    [SerializeField] public string cosmeticName;
    [SerializeField] public Sprite cosmeticImage;
    [SerializeField] public int cosmeticID; //cosmetic IDs here must be the same as they are in the Cosmetic scriptableObjects to work properly
}

