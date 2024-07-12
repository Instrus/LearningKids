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

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

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
        avatarIndex += 1;
        if (avatarIndex == avatars.Count)
            avatarIndex = 0;
        avatar.sprite = avatars[avatarIndex];
    }

    public void AvatarPrev()
    {
        avatarIndex -= 1;
        if (avatarIndex < 0)
            avatarIndex = avatars.Count - 1;
        avatar.sprite = avatars[avatarIndex];
    }

    public void HatNext()
    {
        hatIndex += 1;
        if (hatIndex == hats.Count)
            hatIndex = 0;
        hat.sprite = hats[hatIndex];
    }

    public void HatPrev()
    {
        hatIndex -= 1;
        if (hatIndex < 0)
            avatarIndex = hats.Count - 1;
        hat.sprite = hats[hatIndex];
    }

    public void ShirtNext()
    {
        shirtIndex += 1;
        if (shirtIndex == shirts.Count)
            shirtIndex = 0;
        shirt.sprite = shirts[shirtIndex];
    }

    public void ShirtPrev()
    {
        shirtIndex -= 1;
        if (shirtIndex < 0)
            shirtIndex = shirts.Count - 1;
        shirt.sprite = shirts[shirtIndex];
    }

    // Use buttons to change indices

    // On return, call this function to save changes to PlayerData
}
