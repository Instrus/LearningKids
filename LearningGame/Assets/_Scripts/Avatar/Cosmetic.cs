using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for avatars, hats, clothing

[CreateAssetMenu]
public class Cosmetic : ScriptableObject
{
    public Sprite cosmetic;
    public int ID; // must be a unique ID.
}
