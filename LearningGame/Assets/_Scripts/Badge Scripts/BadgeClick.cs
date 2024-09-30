using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeClick : MonoBehaviour
{
    public Image Badge;

    public Sprite Gscale;
    public Sprite CBadge;

    private bool bClick = false;

    public void click(){
        bClick = !bClick;

        if(bClick){
            
            bClick = true;
            Badge.sprite = CBadge;
    
        }
    
    }
}
