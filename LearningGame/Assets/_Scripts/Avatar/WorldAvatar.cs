using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Players avatar in the world
public class WorldAvatar : MonoBehaviour
{

    [SerializeField] CosmeticsDatabase cosmeticDatabase;

    PlayerData playerData; // for loading avatar cosmetics

    // sprites of avatar
    [SerializeField] public SpriteRenderer avatar;
    [SerializeField] public SpriteRenderer hat;
    [SerializeField] public SpriteRenderer shirt;

    // avatar cosmetics indices
    private int avatarIndex;
    private int hatIndex;
    private int shirtIndex;

    private void Awake()
    {
        gameObject.name = "Player";
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        cosmeticDatabase = Resources.Load<CosmeticsDatabase>("CosmeticsDatabase");
    }

    // Avatar will be enabled when games are enabled. PlayerData should always load first (even though there is currently an order bug atm)
    private void OnEnable()
    {
        LoadAvatarData();
    }

    public void LoadAvatarData()
    {
        int[] cosmeticsIndices = playerData.GetAvatar();

        avatarIndex = cosmeticsIndices[0];
        hatIndex = cosmeticsIndices[1];
        shirtIndex = cosmeticsIndices[2];

        //avatar.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Avatar, avatarIndex);
        //hat.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Hat, hatIndex);
        //shirt.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Clothes, shirtIndex);

        // load from unlocked cosmetics in playerData
        int a = playerData.unlockedAvatarIDs[avatarIndex];
        int b = playerData.unlockedClothesIDs[shirtIndex];
        int c = playerData.unlockedHatsIDs[hatIndex];

        avatar.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Avatar, a);
        shirt.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Clothes, b);
        hat.sprite = cosmeticDatabase.GetCosmetic(CosmeticsDatabase.CosmeticType.Hat, c);
    }

}
