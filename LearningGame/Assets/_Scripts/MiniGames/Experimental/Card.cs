using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string question;
    public string answer;
    [SerializeField] public string[] answerSet; // only applies to FlashCrds, not FIB


    // add a pool of answers later
    // deal also with lower/upper case
}
