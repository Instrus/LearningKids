using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds all cosmetics in the game

// might be able to add this into resource folder

[CreateAssetMenu]
public class CosmeticsDatabase : ScriptableObject
{

    // Avatar_E holds references to PlayerData.

    // maybe public get, private set
    [SerializeField] public Cosmetic[] avatarCosmetics;
    [SerializeField] public Cosmetic[] hatCometics;
    [SerializeField] public Cosmetic[] clothesCosmetics;

    public enum CosmeticType
    {
        None,
        Avatar,
        Hat,
        Clothes
    }



    // methods for loading lists in avatar screen
    // time complexity is large but n is very low. CONSIDER USING A DICTIONARY (use one later but get this working for now)
    public List<Sprite> FetchCosmeticsOfType(CosmeticType cosmeticType, List<int> IDlist)
    {
        List<Sprite> ret = new List<Sprite>();

        // search through every respective list of cosmetics. Load up cosmetics onto ret. return.
        if (cosmeticType == CosmeticType.Avatar)
        {
            // evaluate each ID
            foreach(var ID in IDlist)
            {
                foreach(var avatar in avatarCosmetics)
                {
                    if (ID == avatar.ID)
                    {
                        Debug.Log("Found item");
                        ret.Add(avatar.cosmetic);
                    }
                }
            }
        }

        if (cosmeticType == CosmeticType.Hat)
        {
            foreach(var ID in IDlist)
            {
                foreach(var hat in hatCometics)
                {
                    if (ID == hat.ID)
                    {
                        Debug.Log("Found item");
                        ret.Add(hat.cosmetic);
                    }
                }
            }
        }

        if (cosmeticType == CosmeticType.Clothes)
        {
            foreach (var ID in IDlist)
            {
                foreach(var shirt in clothesCosmetics)
                {
                    if (ID == shirt.ID)
                    {
                        Debug.Log("Found item");
                        ret.Add(shirt.cosmetic);
                    }
                }
            }
        }

        return ret;
    }

    // gets an a certain cosmetic from any of the lists
    public Sprite GetCosmetic(CosmeticType cosmeticType, int ID)
    {
        if (cosmeticType == CosmeticType.Avatar)
        {
            foreach (var avatar in avatarCosmetics)
            {
                if (ID == avatar.ID)
                {
                    return avatar.cosmetic;
                }
            }
        }

        if (cosmeticType == CosmeticType.Hat)
        {
            foreach(var hat in hatCometics)
            {
                if (ID == hat.ID)
                {
                    return hat.cosmetic;
                }
            }
        }

        if (cosmeticType == CosmeticType.Clothes)
        {
            foreach (var shirt in clothesCosmetics)
            {
                if (ID == shirt.ID)
                {
                    return shirt.cosmetic;
                }
            }
        }

        return null;
    }

    public Sprite GetRandomCosmetic(CosmeticType cosmeticType)
    {
        if (cosmeticType == CosmeticType.Avatar)
        {
            int random = Random.Range(0, avatarCosmetics.Length);
            return avatarCosmetics[random].cosmetic;
        }

        if (cosmeticType == CosmeticType.Hat)
        {
            int random = Random.Range(0, hatCometics.Length);
            return hatCometics[random].cosmetic;
        }

        if (cosmeticType == CosmeticType.Clothes)
        {
            int random = Random.Range(0, clothesCosmetics.Length);
            return clothesCosmetics[random].cosmetic;
        }

        return null;
    }

}


// methods for NPCs

// methods for playerAvatar





