using UnityEngine;

[CreateAssetMenu]
public class CardDatabase : ScriptableObject // holds all Cards
{
    [SerializeField] public Card[] FlashCards;
    [SerializeField] public Card[] FIBCards;
}
