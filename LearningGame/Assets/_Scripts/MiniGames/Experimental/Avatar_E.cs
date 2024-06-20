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

    // sprites List Images can use
    [SerializeField] List<Sprite> avatars = new List<Sprite>();
    [SerializeField] List<Sprite> hats = new List<Sprite>();
    [SerializeField] List<Sprite> shirts = new List<Sprite>();

    private void OnEnable()
    {
        avatar.sprite = avatars[1];
        hat.sprite = hats[1];
        shirt.sprite = shirts[1];
    }

    // Use buttons to change indices

    // On return, call this function to save changes to PlayerData
}
