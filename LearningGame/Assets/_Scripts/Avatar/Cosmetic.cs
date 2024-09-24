using UnityEngine;

// for avatars, hats, clothing

[CreateAssetMenu]
public class Cosmetic : ScriptableObject
{
    public Sprite cosmetic;
    public int ID; // must be a unique ID. (must be the same in shopCardData if its a sold item)
}
