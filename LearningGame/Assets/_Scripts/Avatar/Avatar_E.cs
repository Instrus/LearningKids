using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar_E : MonoBehaviour
{
    // OnEnable, load data from PlayerData (indexes)
    // Set the Lists to be assosciated with those indexes

    // reference to image component
    [SerializeField] public Image avatar;
    [SerializeField] public Image hat;
    [SerializeField] public Image shirt;

    // avatar cosmetics indices
    private int avatarIndex;
    private int hatIndex;
    private int shirtIndex;

    // sprites List Images can use
    [SerializeField] List<Sprite> avatars = new List<Sprite>();
    [SerializeField] List<Sprite> hats = new List<Sprite>();
    [SerializeField] List<Sprite> shirts = new List<Sprite>();

    PlayerData playerData;

    CosmeticsDatabase cosmeticsDatabase;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        cosmeticsDatabase = Resources.Load<CosmeticsDatabase>("CosmeticsDatabase");
    }

    private void OnEnable()
    {
        // load list of available cosmetics 
        // load avatars from database using playerdata IDs
        // ... load hats and shirts the same
        LoadCosmetics();

        // then load equiped avatar cosmetics onto avatar
        LoadAvatarData();
    }

    public void LoadCosmetics()
    {
        avatars = cosmeticsDatabase.FetchCosmeticsOfType(CosmeticsDatabase.CosmeticType.Avatar, playerData.unlockedAvatarIDs);
        hats = cosmeticsDatabase.FetchCosmeticsOfType(CosmeticsDatabase.CosmeticType.Hat, playerData.unlockedHatsIDs);
        shirts = cosmeticsDatabase.FetchCosmeticsOfType(CosmeticsDatabase.CosmeticType.Clothes, playerData.unlockedClothesIDs);
    }

    public void LoadAvatarData()
    {
        int[] cosmeticsIndices = playerData.GetAvatar();

        avatarIndex = cosmeticsIndices[0];
        hatIndex = cosmeticsIndices[1];
        shirtIndex = cosmeticsIndices[2];

        avatar.sprite = avatars[avatarIndex];
        hat.sprite = hats[hatIndex];
        shirt.sprite = shirts[shirtIndex];
    }

    // called but return button
    public void SaveAvatarData()
    {
        playerData.SetAvatar(avatarIndex, hatIndex, shirtIndex);
    }

    public void AvatarNext()
    {
        if (avatarIndex < avatars.Count -1 ) avatarIndex += 1;
        avatar.sprite = avatars[avatarIndex];
    }

    public void AvatarPrev()
    {
        if (avatarIndex > 0) avatarIndex -= 1;
        avatar.sprite = avatars[avatarIndex];
    }

    public void HatNext()
    {
        if (hatIndex < hats.Count - 1) hatIndex += 1;
        hat.sprite = hats[hatIndex];
    }

    public void HatPrev()
    {
        if (hatIndex > 0) hatIndex -= 1;
        hat.sprite = hats[hatIndex];
    }

    public void ShirtNext()
    {
        if (shirtIndex < shirts.Count -1) shirtIndex += 1;
        shirt.sprite = shirts[shirtIndex];
    }

    public void ShirtPrev()
    {
        if (shirtIndex > 0) shirtIndex -= 1;
        shirt.sprite = shirts[shirtIndex];
    }

    // Use buttons to change indices

    // On return, call this function to save changes to PlayerData
}
