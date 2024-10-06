using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.iOS;
using UnityEngine;

public class NPC : MonoBehaviour
{
    CosmeticsDatabase cosmeticsDatabase;

    // sprites of avatar
    [SerializeField] public SpriteRenderer avatar;
    [SerializeField] public SpriteRenderer hat;
    [SerializeField] public SpriteRenderer shirt;

    // Avatar will be enabled when games are enabled.
    private void OnEnable()
    {
        gameObject.name = "NPC";
        cosmeticsDatabase = Resources.Load<CosmeticsDatabase>("CosmeticsDatabase");
        GetRandomCosmetics();
        
    }

    public void GetRandomCosmetics()
    {
        avatar.sprite = cosmeticsDatabase.GetRandomCosmetic(CosmeticsDatabase.CosmeticType.Avatar);
        hat.sprite = cosmeticsDatabase.GetRandomCosmetic(CosmeticsDatabase.CosmeticType.Hat);
        shirt.sprite = cosmeticsDatabase.GetRandomCosmetic(CosmeticsDatabase.CosmeticType.Clothes);
    }

}