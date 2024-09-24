using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    // mark true if contact info card.
    // set question, answer, answer index after picked?
    public bool contactInfoCard;
    public string question;
    public string answer;
    public int answerIndex;
    [SerializeField] public string[] answerSet; // only applies to FlashCrds, not FIB


    // add a pool of answers later
    // deal also with lower/upper case
}
