using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNametext;
    // possible reference to profile picture
    // reference music volume slider, sound effects slider (it will change audio manager sound volume too)

    // MIGHT WANT TO USE PLAYER PREFS TO SAVE THE SOUND SETTINGS

    PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    private void OnEnable()
    {
        userNametext.text = playerData.GetUsername();
    }
}
