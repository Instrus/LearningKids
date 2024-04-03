using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Avatar_Container : MonoBehaviour
{
    public Image avatar;
    //public Image[] hats;
    //public Image[] clothes;

    public Sprite[] avatars;

    int avatarIndex = 0;
    //int hatIndex = 0;
    //int clothesIndex = 0;

    // Have positions of the items, like hats, but item 0 needs to be clear image (the null one)

    private void Start()
    {
        avatar = GetComponent<Image>();
    }

    // Functions are called by buttons

    // Function to change avatar
    public void changeAvatar()
    {
        if (avatarIndex == 0)
        {
            avatarIndex++;
            avatar.sprite = avatars[avatarIndex];
        }
        //if (avatarIndex == 1)
        //{
        //    avatarIndex = 0;
        //    avatar.sprite = avatars[0];
        //}
    }
    // Function to change hat
    //public void changeHat()
    //{

    //}

    // Function to change clothes
    //public void changeClothes()
    //{

    //}

}
